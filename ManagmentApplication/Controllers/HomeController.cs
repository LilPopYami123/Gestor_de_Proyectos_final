using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManagmentApplication.Data;
using System.Threading.Tasks;

namespace ManagmentApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly MiContexto _context;

        public HomeController(MiContexto context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var totalProyectos = await _context.Proyectos.CountAsync();
            var totalParticipantes = await _context.Participantes.CountAsync();
            var totalTareas = await _context.Tareas.CountAsync();
            var tareasCompletadas = await _context.Tareas.CountAsync(t => t.Estado == "Completada");
            var tareasPendientes = await _context.Tareas.CountAsync(t => t.Estado == "Pendiente");

            var model = new
            {
                TotalProyectos = totalProyectos,
                TotalParticipantes = totalParticipantes,
                TotalTareas = totalTareas,
                TareasCompletadas = tareasCompletadas,
                TareasPendientes = tareasPendientes
            };

            return View(model);
        }
    }
}
