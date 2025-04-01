using HRMS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;

namespace HRMS.Infrastructure.Data
{
    public class HRMSDbContext : DbContext
    {
        public HRMSDbContext(DbContextOptions<HRMSDbContext> options) : base(options) { }

        // Add this parameterless constructor
        public HRMSDbContext() { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<JobHistory> JobHistories { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Load the connection string manually for EF Migrations
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<Employee>()
                .Property(e => e.InsertedDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Employee>()
                .Property(e => e.UpdatedDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Employee>()
                .Property(j => j.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<JobHistory>()
                .Property(j => j.InsertedDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<JobHistory>()
                .Property(j => j.UpdatedDate)
                .HasDefaultValueSql("GETDATE()");

 
        }
        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Employee || e.Entity is JobHistory &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                if (entry.Entity is Employee employee)
                {
                    if (entry.State == EntityState.Added)
                    {
                        employee.InsertedDate = DateTime.UtcNow;
                    }
                    employee.UpdatedDate = DateTime.UtcNow;
                }

                if (entry.Entity is JobHistory jobHistory)
                {
                    if (entry.State == EntityState.Added)
                    {
                        jobHistory.InsertedDate = DateTime.UtcNow;
                    }
                    jobHistory.UpdatedDate = DateTime.UtcNow;
                }
            }
        }
    }
}
