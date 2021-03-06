﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Core;

namespace WebAPI.Store.Context
{
    public class StoreContext : DbContext
    {
        private readonly string connectionString;
        private readonly string migrationAssemblyName;

        public StoreContext(string connectionString, string migrationAssemblyName)
        {
            this.connectionString = connectionString;
            this.migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString, m => m.MigrationsAssembly(migrationAssemblyName));
            }
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Product> Products { get; set; }

    }
}
