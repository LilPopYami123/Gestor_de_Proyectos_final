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
    public class StatusProyectoesController : Controller
    {
        private readonly MiContexto _context;

        public StatusProyectoesController(MiContexto context)
        {
            _context = context;
        }

        // GET: StatusProyectoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.StatusProyectos.ToListAsync());
        }

        // GET: StatusProyectoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusProyecto = await _context.StatusProyectos
                .FirstOrDefaultAsync(m => m.IdStatus == id);
            if (statusProyecto == null)
            {
                return NotFound();
            }

            return View(statusProyecto);
        }

        // GET: StatusProyectoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatusProyectoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdStatus,Descripcion")] StatusProyecto statusProyecto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statusProyecto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statusProyecto);
        }

        // GET: StatusProyectoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusProyecto = await _context.StatusProyectos.FindAsync(id);
            if (statusProyecto == null)
            {
                return NotFound();
            }
            return View(statusProyecto);
        }

        // POST: StatusProyectoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdStatus,Descripcion")] StatusProyecto statusProyecto)
        {
            if (id != statusProyecto.IdStatus)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statusProyecto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusProyectoExists(statusProyecto.IdStatus))
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
            return View(statusProyecto);
        }

        // GET: StatusProyectoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusProyecto = await _context.StatusProyectos
                .FirstOrDefaultAsync(m => m.IdStatus == id);
            if (statusProyecto == null)
            {
                return NotFound();
            }

            return View(statusProyecto);
        }

        // POST: StatusProyectoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var statusProyecto = await _context.StatusProyectos.FindAsync(id);
            if (statusProyecto != null)
            {
                _context.StatusProyectos.Remove(statusProyecto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusProyectoExists(int id)
        {
            return _context.StatusProyectos.Any(e => e.IdStatus == id);
        }
    }
}
