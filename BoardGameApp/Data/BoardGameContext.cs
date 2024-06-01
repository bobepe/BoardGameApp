using BoardGameApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardGameApp.Data
{
    public class BoardGameContext : DbContext
    {
        public BoardGameContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Play> Play { get; set; }
        public DbSet<PlayPlayer> PlayPlayer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PlayPlayer>()
                .HasKey(pp => new { pp.PlayId, pp.PlayerId, pp.RoleId });

            modelBuilder.Entity<PlayPlayer>()
                .HasOne(pp => pp.Play)
                .WithMany(p => p.PlayerPlays)
                .HasForeignKey(pp => pp.PlayId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PlayPlayer>()
                .HasOne(pp => pp.Player)
                .WithMany(p => p.PlayerPlays)
                .HasForeignKey(pp => pp.PlayerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PlayPlayer>()
                .HasOne(pp => pp.Role)
                .WithMany(p => p.PlayerPlays)
                .HasForeignKey(pp => pp.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            // Game relationships
            modelBuilder.Entity<Game>()
                .HasMany(g => g.Plays)
                .WithOne(p => p.Game)
                .HasForeignKey(p => p.GameId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Game>()
                .HasMany(g => g.Roles)
                .WithOne(r => r.Game)
                .HasForeignKey(r => r.GameId)
                .OnDelete(DeleteBehavior.Restrict);

            // _
            //modelBuilder.Entity<Player>()
            //    .HasMany(p => p.PlayerPlays)
            //    .WithOne(pp => pp.Player)
            //    .HasForeignKey(pp => pp.PlayerId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Play>()
            //    .HasMany(p => p.PlayerPlays)
            //    .WithOne(pp => pp.Play)
            //    .HasForeignKey(pp => pp.PlayId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Role>()
            //    .HasMany(r => r.PlayerPlays)
            //    .WithOne(pp => pp.Role)
            //    .HasForeignKey(pp => pp.RoleId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Game>()
            //    .HasMany(g => g.Plays)
            //    .WithOne(p => p.Game)
            //    .HasForeignKey(p => p.GameId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Game>()
            //    .HasMany(g => g.Roles)
            //    .WithOne(r => r.Game)
            //    .HasForeignKey(r => r.GameId)
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
