using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PruebaWebMaster000.Models;

namespace PruebaWebMaster000.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly BaseMasterContext _context;
        private BaseMasterContext db = new BaseMasterContext();

      
        public HomeController(BaseMasterContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            
            var baseMasterContext = _context.Restaurantes.Include(r => r.IdHorariosNavigation);
            return View(await baseMasterContext.ToListAsync());
        }

        public IActionResult Inicio()
        {
           
           return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
