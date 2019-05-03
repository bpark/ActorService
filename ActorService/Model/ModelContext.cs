using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ActorService.Model
{
    public class ModelContext : DbContext
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Zone> Zones { get; set; }

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
                entity.Property(e => e.Name).IsRequired().HasColumnType("VARCHAR(255)");
                entity.Property(e => e.Quality).HasColumnType("VARCHAR(10)")
                    .HasConversion(
                        v => v.Name,
                        v => Quality.FromName(v));
                entity.Property(e => e.Balance)
                    .HasConversion(
                        v => v.Name,
                        v => Balance.FromName(v));
                entity.Property(e => e.Abilities).HasColumnType("VARCHAR(255)")
                    .HasConversion(
                        v => string.Join(",", v.Select(a => a.Name).ToList()),
                        v => v.Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(Ability.FromName).ToList()
                    );
            });

            modelBuilder.Entity<Zone>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany(e => e.Actors);
                entity.Property(e => e.Name).IsRequired();
                entity.Ignore(e => e.Inhabitants);
                entity.Property(e => e.ZoneType).IsRequired().HasColumnType("VARCHAR(20)")
                    .HasConversion(
                        v => v.Name,
                        v => ZoneType.FromName(v));
            });
        }

    }
}