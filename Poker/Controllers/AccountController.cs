using Microsoft.AspNetCore.Mvc;
using Poker.Data;
using Poker.Models;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Poker.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Username == model.Username))
                {
                    ViewBag.ErrorMessage = "Пользователь с таким именем уже существует.";
                    return View(model);
                }

                var user = new UserModel
                {
                    Username = model.Username,
                    PasswordHash = HashPassword(model.Password),
                    Balance = 1000
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                ViewBag.SuccessMessage = "Registration successful!";
                return View(); 
            }

            return View(model);
        }


        [HttpPost]
        public IActionResult Login(string Username, string Password)
        {
            var hashedPassword = HashPassword(Password);
            var user = _context.Users.FirstOrDefault(u => u.Username == Username && u.PasswordHash == hashedPassword);

            if (user != null)
            {
                TempData["Username"] = user.Username;
                TempData["Balance"] = user.Balance.ToString();  
                TempData["UserId"] = user.Id; 

                TempData["SuccessMessage"] = "Login successful!";
                return RedirectToAction("Game", "Poker");
            }

            ModelState.AddModelError("", "Неверное имя пользователя или пароль.");
            return View("Register");
        }




        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
