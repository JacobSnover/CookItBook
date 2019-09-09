using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace CookItBook.Infrastructure
{
    public class RecipeBook : DbContext
    {
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<Instruction> Instruction { get; set; }
        public DbSet<Recipe> Recipe { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile(@"C:\Users\ShaKy\Source\Repos\CookItBook\CookItBook\appsettings.json")
            .Build();

            optionBuilder.UseSqlServer(configuration.GetConnectionString("CookItBookContextConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>()
                .HasOne(r => r.Recipe)
                .WithMany(i => i.Ingredient)
                .HasForeignKey(r => r.RecipeID);

            modelBuilder.Entity<Instruction>()
                .HasOne(r => r.Recipe)
                .WithMany(i => i.Instruction)
                .HasForeignKey(r => r.RecipeID);
        }
    }
}