using Microsoft.AspNetCore.Mvc;
using PortfolioAdminPanel.Data;

namespace PortfolioAdminPanel.Controllers
{
    public class HomeController : Controller
    {
        // Veritabanı bağlantımızı buraya da çağırıyoruz
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Sadece 'Tamamlandı' (IsCompleted == true) olan projeleri
            // en yeni eklenenden en eskiye doğru sıralayarak (OrderByDescending) vitrine çekiyoruz.
            var completedProjects = _context.Projects
                                            .Where(p => p.IsCompleted)
                                            .OrderByDescending(p => p.CreatedDate)
                                            .ToList();

            // Çektiğimiz bu bitmiş projeleri ana sayfaya gönderiyoruz
            return View(completedProjects);
        }
    }
}