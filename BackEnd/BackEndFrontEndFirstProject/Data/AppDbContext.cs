using BackEndFrontEndFirstProject.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEndFrontEndFirstProject.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ToDo> ToDoList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>().HasData(new ToDo()
            {
                Message = "patates",
                CreateDate = DateTime.Now,
                Id = 1
            },
            new ToDo()
            {
                Message = "çilek",
                CreateDate = DateTime.Now,
                Id = 2
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb; database=BackEndFrontEndFirstProject; trusted_connection=true;");
        }
    }
}
