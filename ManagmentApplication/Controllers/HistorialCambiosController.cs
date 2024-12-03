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
    public class HistorialCambiosController : Controller
    {
        private readonly MiContexto _context;

        public HistorialCambiosController(MiContexto context)
        {
            _context = context;
        }

        // GET: HistorialCambios
        public async Task<IActionResult> Index()
        {
            var miContexto = _context.HistorialCambios.Include(h => h.IdProyectoNavigation);
            return View(await miContexto.ToListAsync());
        }

        // GET: HistorialCambios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialCambio = await _context.HistorialCambios
                .Include(h => h.IdProyectoNavigation)
                .FirstOrDefaultAsync(m => m.IdCambio == id);
            if (historialCambio == null)
            {
                return NotFound();
            }

            return View(historialCambio);
        }

        // GET: HistorialCambios/Create
        public IActionResult Create()
        {
            ViewData["IdProyecto"] = new SelectList(_context.Proyectos, "IdProyecto", "IdProyecto");
            return View();
        }

        // POST: HistorialCambios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCambio,IdProyecto,Descripcion,Fecha")] HistorialCambio historialCambio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historialCambio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProyecto"] = new SelectList(_context.Proyectos, "IdProyecto", "IdProyecto", historialCambio.IdProyecto);
            return View(historialCambio);
        }

        // GET: HistorialCambios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialCambio = await _context.HistorialCambios.FindAsync(id);
            if (historialCambio == null)
            {
                return NotFound();
            }
            ViewData["IdProyecto"] = new SelectList(_context.Proyectos, "IdProyecto", "IdProyecto", historialCambio.IdProyecto);
            return View(historialCambio);
        }

        // POST: HistorialCambios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCambio,IdProyecto,Descripcion,Fecha")] HistorialCambio historialCambio)
        {
            if (id != historialCambio.IdCambio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historialCambio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistorialCambioExists(historialCambio.IdCambio))
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
            ViewData["IdProyecto"] = new SelectList(_context.Proyectos, "IdProyecto", "IdProyecto", historialCambio.IdProyecto);
            return View(historialCambio);
        }

        // GET: HistorialCambios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialCambio = await _context.HistorialCambios
                .Include(h => h.IdProyectoNavigation)
                .FirstOrDefaultAsync(m => m.IdCambio == id);
            if (historialCambio == null)
            {
                return NotFound();
            }

            return View(historialCambio);
        }

        // POST: HistorialCambios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historialCambio = await _context.HistorialCambios.FindAsync(id);
            if (historialCambio != null)
            {
                _context.HistorialCambios.Remove(historialCambio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistorialCambioExists(int id)
        {
            return _context.HistorialCambios.Any(e => e.IdCambio == id);
        }
    }
}
