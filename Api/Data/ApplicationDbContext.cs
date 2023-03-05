using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) // eventuální rozšíření přes konstruktor 
        {
        }
        
        public DbSet<Goal> Goals { get; set; } // přes ApplicationDbContext.Articles se dostaneme ke všem článkům

        protected override void OnModelCreating(ModelBuilder builder) // co se má stát během vytváření modelu
        {
            base.OnModelCreating(builder); // zavolej standardní obsluhu z předka
                                           // a naseeduj následujííc položku, pokud ji už databáze neobsahuje
            builder.Entity<Goal>().HasData(new Goal { Id = 1, Title = "Předefinový goal", Description = "Tento goal byl předem definován", Date = DateTime.Now });
        }
    }
}
