using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WakandaTraining.API.Data;
using WakandaTraining.API.Models;

namespace WakandaTraining.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CursosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Curso>>> Get()
        {
            var cursos = await _context.Cursos.ToListAsync();

            return Ok(cursos);
        }
    }
}