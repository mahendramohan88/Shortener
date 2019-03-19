using Microsoft.Extensions.DependencyInjection;
using Shortener.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shortener.Data.Configuration
{
    public static class DbSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var scopeServiceProvider = serviceScope.ServiceProvider;
                var db = scopeServiceProvider.GetService<AppDbContext>();

                if (db.Database.EnsureCreated())
                {
                    // Seed data goes here

                }
            }
        }
    }
}
