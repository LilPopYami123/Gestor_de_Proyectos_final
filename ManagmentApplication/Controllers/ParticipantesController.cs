using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManagmentApplication.Data;
using ManagmentApplication.Models;

namespace ManagmentApplication.Controllers
{
    public class ParticipantesController : Controller
    {
        private readonly MiContexto _context;

        public ParticipantesController(MiContexto context)
        {
            _context = context;
        }

        // GET: Participantes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Participantes.ToListAsync());
        }

        // GET: Participantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participante = await _context.Participantes
                .FirstOrDefaultAsync(m => m.IdParticipante == id);
            if (participante == null)
            {
                return NotFound();
            }

            return View(participante);
        }

        // GET: Participantes/Create
        // GET: Participantes/Create
        public IActionResult Create()
        {
            ViewBag.Proyectos = new SelectList(_context.Proyectos, "IdProyecto", "Nombre");
            ViewBag.Tareas = new SelectList(_context.Tareas, "IdTarea", "Nombre"); // Cargar todas las tareas
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdParticipante,Nombre,Correo,Telefono")] Participante participante, int TareaId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(participante);
                await _context.SaveChangesAsync();

                var tarea = await _context.Tareas.FindAsync(TareaId);
                if (tarea != null)
                {
                    tarea.IdParticipantes.Add(participante);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(participante);
        }


        // GET: Tareas/GetTareasPorProyecto/5
        // GET: Tareas/GetTareasPorProyecto/5
        [HttpGet]
        public async Task<IActionResult> GetTareasPorProyecto(int id)
        {
            var tareas = await _context.Tareas
                .Where(t => t.IdProyecto == id)
                .Select(t => new { idTarea = t.IdTarea, nombre = t.Nombre })
                .ToListAsync();

            return Json(tareas);
        }




        // GET: Participantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participante = await _context.Participantes.FindAsync(id);
            if (participante == null)
            {
                return NotFound();
            }
            return View(participante);
        }

        // POST: Participantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdParticipante,Nombre,Correo,Telefono")] Participante participante)
        {
            if (id != participante.IdParticipante)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipanteExists(participante.IdParticipante))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(participante);
        }

        // GET: Participantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participante = await _context.Participantes
                .FirstOrDefaultAsync(m => m.IdParticipante == id);
            if (participante == null)
            {
                return NotFound();
            }

            return View(participante);
        }

        // POST: Participantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var participante = await _context.Participantes.FindAsync(id);
            if (participante != null)
            {
                _context.Participantes.Remove(participante);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipanteExists(int id)
        {
            return _context.Participantes.Any(e => e.IdParticipante == id);
        }
    }
}
