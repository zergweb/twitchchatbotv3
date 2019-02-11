using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchBotV3.Model;
using TwitchBotV3.Model.Repositories;

namespace TwitchBotV3.DbInit
{
    public class DbInitializer
    {
        public static void InitDb(IApplicationBuilder appBuilder)
        {
            using (var serviceScope = appBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                using (context)
                {
                    context.Database.Migrate();
                    if (!context.Persons.Any())
                    {
                        var bands = new List<Person>()
                        {
                            new Person(){
                                Id=1,
                                Name = "person 1",
                                Login="admin",
                                Password="1234",
                                Role=new Role(){Id=1, RoleName="admin"}
                            },
                            new Person(){
                                Id=2,
                                Name = "person 2",
                                Login="user",
                                Password="1234",
                                Role=new Role(){Id=2, RoleName="user"}
                            }
                        };
                        context.Persons.AddRange(bands);
                        context.SaveChanges();
                    }
                }
            }

        }
    }
}
