using ArchLab4;
using Arch4Platform;
using Microsoft.EntityFrameworkCore;

namespace ArchLab4.Repository
{
    public class ArchDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public ArchDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasMany<TaskEntity>(em => em.Tasks)
                .WithOne(task => task.Employee)
                .HasForeignKey(task => task.EmployeeId)
                .IsRequired(false);

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, Name = "John", Title = "Manager", IsActive = true },
                new Employee { Id = 2, Name = "Alice", Title = "Developer", IsActive = true }
                );

            modelBuilder.Entity<TaskEntity>().HasData(
                new TaskEntity { Id = 2, Name = "Task 2", Description = "Description 2", IsCompleted = true, EmployeeId = 2 },
                new TaskEntity { Id = 1, Name = "Task 1", Description = "Description 1", IsCompleted = false, EmployeeId = 1 },
                new TaskEntity { Id = 3, Name = "Task 3", Description = "Description 3", IsCompleted = false, EmployeeId = 1 },
                new TaskEntity { Id = 4, Name = "Task 4", Description = "Description 4", IsCompleted = true, EmployeeId = 2 }
                );
        }


    }
}
