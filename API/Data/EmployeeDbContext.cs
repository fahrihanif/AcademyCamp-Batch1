using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class EmployeeDbContext : DbContext
{
    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) { }

    public DbSet<Country> Countries { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<History> Histories { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<History>(history => history.HasKey(key => new {
            key.EmployeeId,
            key.StartDate
        }));

        builder.Entity<Employee>().HasIndex(employee => employee.PhoneNumber).IsUnique();
        builder.Entity<Employee>().HasIndex(employee => employee.Email).IsUnique();
        builder.Entity<User>().HasIndex(user => user.UserName).IsUnique();

        builder.Entity<Region>()
               .HasMany(region => region.Countries)
               .WithOne(country => country.Region)
               .HasForeignKey(country => country.RegionId);

        builder.Entity<Country>()
               .HasMany(country => country.Locations)
               .WithOne(location => location.Country)
               .HasForeignKey(location => location.CountryId);

        builder.Entity<Department>()
               .HasOne(department => department.Location)
               .WithMany(location => location.Departments)
               .HasForeignKey(department => department.LocationId);

        builder.Entity<Department>()
               .HasOne(department => department.Manager)
               .WithOne(manager => manager.DepartmentManager)
               .HasForeignKey<Department>(department => department.ManagerId);

        builder.Entity<Department>()
               .HasMany(department => department.Histories)
               .WithOne(history => history.Department)
               .HasForeignKey(history => history.DepartmentId);

        builder.Entity<Role>()
               .HasMany(role => role.UserRoles)
               .WithOne(userRole => userRole.Role)
               .HasForeignKey(userRole => userRole.RoleId);

        builder.Entity<User>()
               .HasMany(user => user.UserRoles)
               .WithOne(userRole => userRole.User)
               .HasForeignKey(userRole => userRole.EmployeeId);

        builder.Entity<User>()
               .HasOne(user => user.Employee)
               .WithOne(employee => employee.User)
               .HasForeignKey<User>(user => user.EmployeeId);

        builder.Entity<Job>()
               .HasMany(job => job.Employees)
               .WithOne(employee => employee.Job)
               .HasForeignKey(employee => employee.JobId);

        builder.Entity<Job>()
               .HasMany(job => job.Histories)
               .WithOne(history => history.Job)
               .HasForeignKey(history => history.JobId);

        builder.Entity<Employee>()
               .HasMany(employee => employee.Histories)
               .WithOne(history => history.Employee)
               .HasForeignKey(history => history.EmployeeId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Employee>()
               .HasOne(employee => employee.Department)
               .WithMany(department => department.Employees)
               .HasForeignKey(employee => employee.DepartmentId);

        builder.Entity<Employee>()
               .HasMany(manager => manager.Employees)
               .WithOne(employee => employee.Manager)
               .HasForeignKey(employee => employee.ManagerId);
    }
}
