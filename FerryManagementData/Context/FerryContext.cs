using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using FerryManagementData.Models;


namespace FerryManagementData.Context
{
    public class FerryContext : DbContext
    {

        static FerryContext()
        {
            Database.SetInitializer(new FerryInitializer());
        }

        //kunne ikke få forbindelse til databasen i appconfig, så jeg har hardcodet forbindelsen
        public FerryContext() : base("Server=.\\SQLEXPRESS;Database=Eksamensprojekt;Trusted_Connection=True;")
        {
           
        }

        public DbSet<Ferry> Ferries { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
