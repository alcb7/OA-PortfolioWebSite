using Microsoft.AspNetCore.Mvc;
using OA.PortfolioWebSite.AdminMVC.Response;
using OA.PortfolioWebSite.AdminMVC.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace OA.PortfolioWebSite.AdminMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _authApiUrl = "https://localhost:7281/api/Auth";

        public AuthController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_authApiUrl}/authenticate", loginDto);

            if (response.IsSuccessStatusCode)
            {
                // JSON yanıtını parse et
                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenJson = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenResponse>(responseContent);

                // Token'ı çıkart ve kontrol et
                var token = tokenJson?.Token;

                if (string.IsNullOrEmpty(token) || !token.Contains("."))
                {
                    ModelState.AddModelError(string.Empty, "Geçersiz token alındı.");
                    return View(loginDto);
                }

                // Token'ı cookie'de sakla
                HttpContext.Response.Cookies.Append("JWToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(60)
                });

                // _httpClient.DefaultRequestHeaders.Authorization satırını kaldırabilirsiniz.
                // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                var role = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "role")?.Value;

                if (role != "admin")
                {
                    ModelState.AddModelError(string.Empty, "Bu alana erişim izniniz yok.");
                    return View(loginDto);
                }

                return RedirectToAction("Privacy", "Home");
            }

            ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı.");
            return View(loginDto);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_authApiUrl}/register", registerDto);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }

            ModelState.AddModelError(string.Empty, "Kayıt işlemi başarısız.");
            return View(registerDto);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Token'ı cookie'den sil
            HttpContext.Response.Cookies.Delete("JWToken");
            return RedirectToAction("Login");
        }
    }
}
