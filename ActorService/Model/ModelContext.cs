using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ActorService.Model
{
    public class ModelContext : DbContext
    {
        public DbSet<Actor> Actors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=192.168.33.11;database=actor;user=actor_user;password=user_actor");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Quality)
                    .HasConversion(
                        v => v.Name,
                        v => Quality.FromName(v));
                entity.Property(e => e.Balance)
                    .HasConversion(
                        v => v.Name,
                        v => Balance.FromName(v));
                entity.Property(e => e.Abilities)
                    .HasConversion(
                        v => string.Join(",", v.Select(a => a.Name).ToList()),
                        v => v.Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(Ability.FromName).ToList()
                    );
            });
        }

    }
}