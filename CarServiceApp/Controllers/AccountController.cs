using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarServiceApp.Models;
using System.Web.Helpers;

namespace CarServiceApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly CarServiceDbContext _context = new CarServiceDbContext();

        public ActionResult Login() => View();

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                Session["UserId"] = user.Id;
                return RedirectToAction("Index", "Service");
            }

            ViewBag.Error = "Invalid credentials.";
            return View();
        }

        public ActionResult SignUp() => View();

        [HttpPost]
        public ActionResult SignUp(string email, string password, string fullName)
        {
            if (_context.Users.Any(u => u.Email == email))
            {
                ViewBag.Error = "Email already exists.";
                return View();
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            _context.Users.Add(new User { Email = email, PasswordHash = hashedPassword, FullName = fullName });
            _context.SaveChanges();

            return RedirectToAction("Login");
        }
    }
}