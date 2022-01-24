// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using LyteVentures.Todo.DataStorageLayers;
using LyteVentures.Todo.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace LyteVentures.Todo.IdentityServer
{
    public class SeedData
    {
        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                    context.Database.Migrate();

                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var demoUser = userMgr.FindByEmailAsync("ghulamcyber@hotmail.com").Result;
                    if (demoUser == null)
                    {
                        demoUser = new ApplicationUser
                        {
                            UserName = "ghulamcyber@hotmail.com",
                            Email = "ghulamcyber@hotmail.com",
                            EmailConfirmed = true,
                            FullName = "Mirza Ghulam Rasyid"
                        };
                        var result = userMgr.CreateAsync(demoUser, "@Future30").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(demoUser, new Claim[]{
                            new Claim(JwtClaimTypes.Name, demoUser.FullName),
                            new Claim(JwtClaimTypes.Email, demoUser.Email)
                        }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Debug("Demo user 'ghulamcyber@hotmail.com' been created successfully");
                    }
                    else
                    {
                        Log.Debug("Demo user 'ghulamcyber@hotmail.com' already exists - skip creating duplicate one");
                    }
                }
            }
        }
    }
}
