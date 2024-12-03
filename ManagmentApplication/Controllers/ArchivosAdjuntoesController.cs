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
    public class ArchivosAdjuntoesController : Controller
    {
        private readonly MiContexto _context;

        public ArchivosAdjuntoesController(MiContexto context)
        {
            _context = context;
        }

        // GET: ArchivosAdjuntoes
        public async Task<IActionResult> Index()
        {
            var miContexto = _context.ArchivosAdjuntos.Include(a => a.IdTareaNavigation);
            return View(await miContexto.ToListAsync());
        }

        // GET: ArchivosAdjuntoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var archivosAdjunto = await _context.ArchivosAdjuntos
                .Include(a => a.IdTareaNavigation)
                .FirstOrDefaultAsync(m => m.IdArchivo == id);
            if (archivosAdjunto == null)
            {
                return NotFound();
            }

            return View(archivosAdjunto);
        }

        // GET: ArchivosAdjuntoes/Create
        public IActionResult Create()
        {
            ViewData["IdTarea"] = new SelectList(_context.Tareas, "IdTarea", "IdTarea");
            return View();
        }

        // POST: ArchivosAdjuntoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdArchivo,IdTarea,NombreArchivo,RutaArchivo,FechaSubida")] ArchivosAdjunto archivosAdjunto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(archivosAdjunto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTarea"] = new SelectList(_context.Tareas, "IdTarea", "IdTarea", archivosAdjunto.IdTarea);
            return View(archivosAdjunto);
        }

        // GET: ArchivosAdjuntoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var archivosAdjunto = await _context.ArchivosAdjuntos.FindAsync(id);
            if (archivosAdjunto == null)
            {
                return NotFound();
            }
            ViewData["IdTarea"] = new SelectList(_context.Tareas, "IdTarea", "IdTarea", archivosAdjunto.IdTarea);
            return View(archivosAdjunto);
        }

        // POST: ArchivosAdjuntoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdArchivo,IdTarea,NombreArchivo,RutaArchivo,FechaSubida")] ArchivosAdjunto archivosAdjunto)
        {
            if (id != archivosAdjunto.IdArchivo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(archivosAdjunto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArchivosAdjuntoExists(archivosAdjunto.IdArchivo))
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
            ViewData["IdTarea"] = new SelectList(_context.Tareas, "IdTarea", "IdTarea", archivosAdjunto.IdTarea);
            return View(archivosAdjunto);
        }

        // GET: ArchivosAdjuntoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var archivosAdjunto = await _context.ArchivosAdjuntos
                .Include(a => a.IdTareaNavigation)
                .FirstOrDefaultAsync(m => m.IdArchivo == id);
            if (archivosAdjunto == null)
            {
                return NotFound();
            }

            return View(archivosAdjunto);
        }

        // POST: ArchivosAdjuntoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var archivosAdjunto = await _context.ArchivosAdjuntos.FindAsync(id);
            if (archivosAdjunto != null)
            {
                _context.ArchivosAdjuntos.Remove(archivosAdjunto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArchivosAdjuntoExists(int id)
        {
            return _context.ArchivosAdjuntos.Any(e => e.IdArchivo == id);
        }
    }
}
