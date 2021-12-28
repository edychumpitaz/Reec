using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Reec.Api.Test.Nuget.DbLegacyTest
{
    public class TestDbContext : DbContext
    {

        public DbSet<Persona> Personas { get; set; }

        public TestDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
             
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Persona>();
        }

    }

}
