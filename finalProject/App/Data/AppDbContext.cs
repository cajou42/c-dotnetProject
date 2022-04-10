using Microsoft.EntityFrameworkCore;
using App.Models;
namespace App.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Car> Car { get; set; }
        public DbSet<Race> Races {get;set;}
        public DbSet<Pilot> Pilots {get;set;}
        public DbSet<RaceResult> RaceResults { get; set; }
        public DbSet<ResultLine> ResultLines { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder){

        }
    }
}