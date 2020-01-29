using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JobPad.Models;
using Microsoft.AspNetCore.Identity;
using JobPad.Data;

namespace JobPad.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private UserManager<ApplicationUser> _userManager;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager
            )
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        // Private method to get current user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

            public IActionResult Index()
        {
            var currentUser = _userManager.GetUserId(HttpContext.User);

            ViewData["currentUser"] = currentUser;
            return View();
        }

        public IActionResult Privacy()
        {
            var currentUser = _userManager.GetUserId(HttpContext.User);

            ViewData["currentUser"] = currentUser;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
