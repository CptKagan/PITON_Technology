using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace PITON_Project.Data
{
    public class AppDbContext : DbContext{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        public DbSet<User> Users {get; set;}
        public DbSet<TaskItem> Tasks {get; set;}
    }
}