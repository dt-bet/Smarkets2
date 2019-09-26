using Smarkets.Entity.NHibernate;
using System.Data.Entity;
using System;
using System.Collections.Generic;

namespace Smarkets.DAL
{
    public class OddsContext : DbContext
    {
        public OddsContext() : base("OddsDB-EF6CodeFirst")
        {
            base.Configuration.AutoDetectChangesEnabled = false;
            base.Configuration.ValidateOnSaveEnabled = false;
            //Database.SetInitializer<SchoolContext>(new SchoolDBInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Adds configurations for Student from separate class
            //modelBuilder.Configurations.Add(new StudentConfigurations());

            //modelBuilder.Entity<Teacher>()
            //    .ToTable("TeacherInfo");

            //modelBuilder.Entity<Teacher>()
            //    .MapToStoredProcedures();

        }

        public DbSet<EventParent> EventParents { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Price> Prices { get; set; }


    }
}