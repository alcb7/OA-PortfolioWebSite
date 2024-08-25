using Microsoft.AspNetCore.Mvc;
using OA.PortfolioWebSite.UserMVC.Responses;
using OA.PortfolioWebSite.UserMVC.ViewModels;
using System.IdentityModel.Tokens.Jwt;

namespace OA.PortfolioWebSite.UserMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _authApiUrl = "https://authapi.digigokali.com.tr/api/Auth";

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

             
                return RedirectToAction("Index", "Home");
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
            return RedirectToAction("Login" , "Auth");
        }
    }
}
