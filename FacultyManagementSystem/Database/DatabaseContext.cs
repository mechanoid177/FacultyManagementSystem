using FacultyManagementSystem.Faculty;
using FacultyManagementSystem.Library;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacultyManagementSystem.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(IConfiguration configuration) : base()
        {
            _configuration = configuration;
        }

        IConfiguration _configuration;

        public DbSet<Book> Books { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = _configuration.GetSection("DatabaseInfo")["ConnectionString"];
                ServerVersion sv = ServerVersion.AutoDetect(connectionString);
                optionsBuilder.UseMySql(connectionString, sv);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Barcode);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Author).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ISBN).IsRequired().HasMaxLength(13);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.BookBarcode).IsRequired();
                entity.Property(e => e.MemberBarcode).IsRequired();
                entity.Property(e => e.BorrowDate).IsRequired();
                entity.Property(e => e.DueDate).IsRequired();
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Barcode);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(15);
                entity.Property(e => e.Surname).IsRequired().HasMaxLength(15);
                entity.Property(e => e.JMBG).IsRequired().HasMaxLength(13);
                entity.Ignore(e => e.Id);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Barcode);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(15);
                entity.Property(e => e.Surname).IsRequired().HasMaxLength(15);
                entity.Property(e => e.JMBG).IsRequired().HasMaxLength(13);
                entity.Ignore(e => e.Id);
            });
        }
    }
}
