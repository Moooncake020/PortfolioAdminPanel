using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PortfolioAdminPanel.Controllers
{
    public class AuthController : Controller
    {
        // Giriş ekranını (View) açar
        public IActionResult Login()
        {
            return View();
        }

        // Formdan gelen kullanıcı adı ve şifreyi kontrol eder
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Sabit bir yönetici hesabı belirliyoruz (Geliştirme aşaması için)
            if (username == "admin" && password == "123456")
            {
                // Kimlik bilgilerini oluştur
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Tarayıcıya güvenli çerezi (Cookie) bırak ve sisteme giriş yap
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                // Giriş başarılıysa projelerin listelendiği panele yönlendir
                return RedirectToAction("Index", "Project");
            }

            // Hatalıysa ekrana uyarı mesajı gönder
            ViewBag.Error = "Kullanıcı adı veya şifre hatalı!";
            return View();
        }

        // Çıkış yapma işlemi
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}