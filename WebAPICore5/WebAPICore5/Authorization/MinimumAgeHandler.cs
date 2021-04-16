using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPICore5.Authorization
{
    public class MinimumAgeHandler : AuthorizationHandler<MinimumageRequirement>
    {
        private readonly ILogger<MinimumAgeHandler> logger;

        public MinimumAgeHandler(ILogger<MinimumAgeHandler> logger)
        {
            this.logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumageRequirement requirement)
        {
            var userEmail = context.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;

            var dateOfBirth = DateTime.Parse(context.User.FindFirst(c => c.Type == "DateOfBirth").Value);

            logger.LogInformation($"Handling minimum age requirement for : {userEmail}. [dateOfBirth : {dateOfBirth}]");


            if (dateOfBirth.AddYears(requirement.MinimumAge) <= DateTime.Today)
            {
                logger.LogInformation("Access granted");
                context.Succeed(requirement);

            }
            else
            {
                logger.LogInformation("Access denied");

            }

            return Task.CompletedTask;
        }
    }
}
