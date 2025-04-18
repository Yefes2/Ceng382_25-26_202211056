using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using RazorPagesProject.Models;
using System.IO;
using System.Text.Json;

namespace RazorPagesProject.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public User Credentials { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            // 1) Load users.json
            var json = System.IO.File.ReadAllText(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/data/users.json"));
            var users = JsonSerializer.Deserialize<List<User>>(json);

            // 2) Validate credentials
            var user = users.FirstOrDefault(u =>
                u.Username == Credentials.Username &&
                u.Password == Credentials.Password &&
                u.IsActive);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Username or password is incorrect.");
                return Page();
            }

            // 3) Generate simple token (GUID)
            var token = Guid.NewGuid().ToString();

            // 4) Store in session
            HttpContext.Session.SetString("username", user.Username);
            HttpContext.Session.SetString("token", token);
            HttpContext.Session.SetString("session_id", HttpContext.Session.Id);

            // 5) Store in cookies
            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddMinutes(30),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };
            Response.Cookies.Append("username", user.Username, cookieOptions);
            Response.Cookies.Append("token", token, cookieOptions);
            Response.Cookies.Append("session_id", HttpContext.Session.Id, cookieOptions);

            // 6) Redirect to your table page (Index)
            return RedirectToPage("/Index");
        }
    }
}
