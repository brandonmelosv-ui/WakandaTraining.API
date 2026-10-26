using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WakandaTraining.API.Data;

namespace WakandaTraining.API.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var cursos = await _context.Cursos.ToListAsync();

            return View(cursos);
        }
    }
}