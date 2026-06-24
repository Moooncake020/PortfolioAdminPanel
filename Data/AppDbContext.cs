using Microsoft.EntityFrameworkCore;
using PortfolioAdminPanel.Models;

namespace PortfolioAdminPanel.Data
{
    // Sınıfımızın EF Core'un özelliklerini miras alması için DbContext'ten türetiyoruz.
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Veritabanımızda 'Projects' adında bir tablo oluşturulacağını belirtiyoruz.
        public DbSet<ProjectItem> Projects { get; set; }
    }
}