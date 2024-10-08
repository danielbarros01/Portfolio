﻿using Microsoft.EntityFrameworkCore;
using Portfolio.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TechnicalSkill> TechnicalSkills { get; set; }
        public DbSet<SoftSkill> SoftSkills { get; set; }
        public DbSet<SoftSkillsTechnologies> SoftSkillsTechnologies { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectsTechnologies> ProjectsTechnologies { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<EducationsTechnologies> EducationsTechnologies { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SoftSkillsTechnologies>()
                .HasKey(x => new { x.AssociationId, x.TechnologyId });

            modelBuilder.Entity<ProjectsTechnologies>()
                .HasKey(pt => new { pt.AssociationId, pt.TechnologyId });

            modelBuilder.Entity<EducationsTechnologies>()
                .HasKey(pt => new { pt.AssociationId, pt.TechnologyId });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                .EnableSensitiveDataLogging();
        }

    }
}
