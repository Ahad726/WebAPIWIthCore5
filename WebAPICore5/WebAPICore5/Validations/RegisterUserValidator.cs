using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Store.IServices;
using WebAPICore5.Models;

namespace WebAPICore5.Validations
{
    // this validation will automatically occure in model state checking in controller class register method. we can add our custom check here
    //seperately. we can also do some unittest on this validation.
    public class RegisterUserValidator : AbstractValidator<RegisterModel>
    {
        public RegisterUserValidator(IUserService userService)
        {
            RuleFor(e => e.Email).NotEmpty();
            RuleFor(p => p.Password).MinimumLength(6);
            RuleFor(p => p.Password).Equal(x => x.ConfirmPassword);

            // value indicate the field we are imposing validation rule. here the field is Email.
            // context is the overall validation context where we can optionally add some validation

            RuleFor(e => e.Email).Custom((value, context) =>
            {
                var userAlreadyExists = userService.GetUser(value);
                if (userAlreadyExists != null)
                {
                    context.AddFailure("Email", "Email address is already taken");

                }
            });
        }
    }
}
