using AdminPageMVC.Entities;
using Microsoft.EntityFrameworkCore;
using Task = AdminPageMVC.Entities.Task;

namespace AdminPageMVC.Data;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    private const string CONNECTION_STRING = "Host=localhost;Port=5432;" +
       "Username=postgres;" +
       "Password=root;" +
       "Database=adminpagemvc";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(CONNECTION_STRING);
    }


    public DbSet<User> Users { get; set; }
    public DbSet<Test> Tests { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<TaskAnswer> TaskAnswers { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Study> Studies { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Result> Results { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Homework> Homeworks { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Contact> Contacts { get; set; }

}

