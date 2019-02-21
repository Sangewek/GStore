using GameStore.DAL.EF.Configuration;
using GameStore.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using GameStore.DAL.Interfaces;

namespace GameStore.DAL.EF
{
    public class DataContext : DbContext,IDataContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
            DataInitialization.Initialize(this);
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Game> Games { get; set; }

        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Platform> Platform { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GenreConfig());
            modelBuilder.ApplyConfiguration(new PlatformConfig());
            modelBuilder.ApplyConfiguration(new PublisherConfig());
            modelBuilder.ApplyConfiguration(new CommentConfig());
            modelBuilder.ApplyConfiguration(new GameConfig());
            modelBuilder.ApplyConfiguration(new GameGenresConfig());
            modelBuilder.ApplyConfiguration(new GamePlatformsConfig());
        }
    } } 
