using Entities.Concretes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=APBS_Database;Trusted_Connection=true;");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>().HasIndex(x => x.Email).IsUnique();
            builder.Entity<AppUser>().HasIndex(x => x.NationalityId).IsUnique();
            builder.Entity<AppUser>().HasIndex(x => x.PhoneNumber).IsUnique();

            base.OnModelCreating(builder);
        }
    }
}
