using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using livraria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace livraria.Controllers
{
    [Route("[controller]")]
    public class LivrariaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LivrariaController> _logger;

        public LivrariaController(ILogger<LivrariaController> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _context = applicationDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Livro livro)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(livro);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Algo deu errado {ex.Message}");
                    throw;
                }
            }

            ModelState.AddModelError(string.Empty, $"Algo deu errado, modelo inválido");
            return View(livro);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}