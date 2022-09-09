using Microsoft.EntityFrameworkCore;
using MVCWebApp.Models.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
       //model to be translated  to the database
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //default seeding 
            modelBuilder.Entity<Person>().HasData(new Person { ID = 1, Name = "Abel Magicho", City = "gothenburg", PhoneNumber = "0743675431" });
            modelBuilder.Entity<Person>().HasData(new Person { ID = 2, Name = "Josefin  Larsson", City = "Stockholm", PhoneNumber = "0743345434" });
            modelBuilder.Entity<Person>().HasData(new Person { ID = 3, Name = "yonas  walters", City = "dc", PhoneNumber = "0143345444" });
        }
    }
}
