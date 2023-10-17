using AdminPageMVC.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task = AdminPageMVC.Entities.Task;

namespace AdminPageMVC.Data;


public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }






    public DbSet<User> Users { get; set; }
    public DbSet<Test> Tests { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<TaskAnswer> TaskAnswers { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Study> Studies { get; set; }
    public DbSet<Review> Feedback { get; set; }
    public DbSet<Result> Results { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Homework> Homeworks { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Contact> Contacts { get; set; }


}

