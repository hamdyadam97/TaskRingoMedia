using Microsoft.EntityFrameworkCore;
using RingoMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RingoMedia.Context
{
    public class RingoMediaDbContext : DbContext

    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public RingoMediaDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasMany(d => d.SubDepartments)
                .WithOne(d => d.ParentDepartment)
                .HasForeignKey(d => d.ParentDepartmentId);
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
