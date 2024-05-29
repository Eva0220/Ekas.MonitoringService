using Ekas.Monitoring.Models;
using Ekas.Monitoring.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ekas.Monitoring
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-TB6CE81;Database=Monitoring_Diplom;Trusted_Connection=True;TrustServerCertificate=true");
        }
    }
}