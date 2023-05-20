using Microsoft.EntityFrameworkCore;
// using dotnetapi.Models.Context;
using NTR.Common.DataAccess.Entities;
using NTR.Common.DataAccess.Context;

namespace dotnetapi.Models.User
{
	public class DBUserContext : EntityContextBase<DBUserContext>
    {
        private readonly IConfiguration configuration;

        // public DbSet<Company>? Company { get; set; }

        public DBUserContext(DbContextOptions<DBUserContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.configuration.GetConnectionString("UserConnection"));
        }
    }
}

