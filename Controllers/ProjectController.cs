using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioAdminPanel.Data;
using PortfolioAdminPanel.Models;

namespace PortfolioAdminPanel.Controllers
{
    [Authorize] // Bu etiket, bu sınıftaki hiçbir metoda giriş yapmadan erişilemeyeceğini belirtir
   
    public class ProjectController : Controller
    {
        // Veritabanına erişmek için az önce oluşturduğumuz bağlamı (Context) çağırıyoruz.
        private readonly AppDbContext _context;

        // Constructor (Yapıcı Metot) ile veritabanı bağlantısını bu sınıfa enjekte ediyoruz.
        public ProjectController(AppDbContext context)
        {
            _context = context;
        }

        // 1. LİSTELEME EKRANI (Read)
        // Tarayıcıdan "/Project" veya "/Project/Index" adresine gidildiğinde bu metot çalışır.
        public IActionResult Index()
        {
            // Veritabanındaki 'Projects' tablosundaki tüm verileri liste halinde çekiyoruz.
            var projects = _context.Projects.ToList();

            // Çektiğimiz bu listeyi, görüntülenebilmesi için View (Arayüz) katmanına gönderiyoruz.
            return View(projects);
        }

        // 2. YENİ KAYIT EKRANINI AÇMA (GET)
        // Sadece boş formu ekrana getirmekle görevlidir.
        public IActionResult Create()
        {
            return View();
        }

        // 3. YENİ KAYDI VERİTABANINA KAYDETME (POST)
        // Formdaki "Kaydet" butonuna basıldığında veriler buraya düşer.
        [HttpPost]
        public IActionResult Create(ProjectItem project)
        {
            // Kullanıcı zorunlu alanları doğru doldurdu mu diye kontrol ediyoruz
            if (ModelState.IsValid)
            {
                // Veriyi EF Core aracılığıyla 'Projects' tablosuna ekliyoruz
                _context.Projects.Add(project);
                // Değişiklikleri veritabanına kalıcı olarak yansıtıyoruz
                _context.SaveChanges();

                // İşlem bitince kullanıcıyı tekrar listeleme ekranına (Index) yönlendiriyoruz
                return RedirectToAction("Index");
            }

            // Eğer formda bir hata varsa, kullanıcının girdiği verilerle birlikte formu tekrar göster
            return View(project);
        }

        // 4. DÜZENLEME EKRANINI AÇMA (GET)
        // Düzenlenecek projenin mevcut verilerini forma doldurmak için ID ile çağrılır.
        public IActionResult Edit(int id)
        {
            // Veritabanında bu ID'ye sahip projeyi buluyoruz.
            var project = _context.Projects.Find(id);

            // Eğer veritabanında böyle bir proje yoksa hata döndür.
            if (project == null)
            {
                return NotFound();
            }

            // Proje bulunduysa, verileriyle birlikte Edit formuna gönder.
            return View(project);
        }

        // 5. GÜNCELLEMELERİ KAYDETME (POST)
        [HttpPost]
        public IActionResult Edit(ProjectItem project)
        {
            if (ModelState.IsValid)
            {
                // Değişen verileri güncellenecek şekilde işaretliyoruz.
                _context.Projects.Update(project);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(project);
        }

        // 6. PROJE SİLME İŞLEMİ
        // Listeleme ekranındaki "Sil" butonuna basıldığında doğrudan burası tetiklenir.
        public IActionResult Delete(int id)
        {
            var project = _context.Projects.Find(id);

            if (project != null)
            {
                // Projeyi tablodan kaldırıyoruz.
                _context.Projects.Remove(project);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}