using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TwitchBotV3.Model;

namespace TwitchBotV3.Model.Repositories
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Role> Roles { get; set; }
        private IConfiguration config;
        public AppDbContext(IConfiguration configuration)
        {
            config = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .UseMySql(config.GetSection("SqlConnections:LocalMysql").Value,
            b => b.MigrationsAssembly("TwitchBotV3")
            );
        }
    }
}
