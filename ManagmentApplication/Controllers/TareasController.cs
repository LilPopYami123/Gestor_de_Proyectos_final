using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManagmentApplication.Data;
using ManagmentApplication.Models;
using OfficeOpenXml;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace ManagmentApplication.Controllers
{
    public class TareasController : Controller
    {
        private readonly MiContexto _context;

        public TareasController(MiContexto context)
        {
            _context = context;
        }

        // GET: Tareas/Create
        public IActionResult Create()
        {
            ViewData["IdProyecto"] = new SelectList(_context.Proyectos, "IdProyecto", "Nombre");
            return View();
        }



        // Método para generar Excel
        public async Task<IActionResult> CrearExcel()
        {
            // Obtener las tareas con los datos necesarios
            var tareasReporte = await _context.Tareas.Include(t => t.IdProyectoNavigation)
                                                      .Include(t => t.IdParticipantes)
                                                      .ToListAsync();

            // Crear un archivo de Excel en memoria
            using (var package = new ExcelPackage())
            {
                // Crear una hoja de trabajo
                var worksheet = package.Workbook.Worksheets.Add("Reporte de Tareas");

                // Agregar los encabezados de las columnas
                worksheet.Cells[1, 1].Value = "Nombre de la Tarea";
                worksheet.Cells[1, 2].Value = "Nombre del Proyecto";
                worksheet.Cells[1, 3].Value = "Participante(s)";
                worksheet.Cells[1, 4].Value = "Tiempo Esperado";

                // Rellenar las filas con los datos de las tareas
                int row = 2;
                foreach (var tarea in tareasReporte)
                {
                    worksheet.Cells[row, 1].Value = tarea.Nombre;
                    worksheet.Cells[row, 2].Value = tarea.IdProyectoNavigation.Nombre;
                    worksheet.Cells[row, 3].Value = string.Join(", ", tarea.IdParticipantes.Select(p => p.Nombre));
                    worksheet.Cells[row, 4].Value = tarea.TiempoEsperado;
                    row++;
                }

                // Convertir el paquete de Excel en un archivo en memoria
                var fileContents = package.GetAsByteArray();

                // Devolver el archivo Excel al usuario
                return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "reporte_tareas.xlsx");
            }
        }

        // POST: Tareas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTarea,IdProyecto,Nombre,Descripcion,TiempoRealizado,TiempoEsperado,FechaCreacion,Estado")] Tarea tarea)
        {
            
            
                // Verifica que 'Estado' tenga un valor válido
                if (string.IsNullOrWhiteSpace(tarea.Estado))
                {
                    tarea.Estado = "Pendiente"; // Valor por defecto
                }

                // Aquí buscamos el Proyecto correspondiente
                var proyecto = await _context.Proyectos.FindAsync(tarea.IdProyecto);
                if (proyecto != null)
                {
                    // Asignamos el Proyecto a la propiedad de navegación
                    tarea.IdProyectoNavigation = proyecto;
                }
                else
                {
                    // Si no encontramos el Proyecto, devolvemos un error o mensaje adecuado
                    ModelState.AddModelError("", "El proyecto no existe.");
                    ViewData["IdProyecto"] = new SelectList(_context.Proyectos, "IdProyecto", "IdProyecto", tarea.IdProyecto);
                    return View(tarea);
                }

                // Agregar la tarea al contexto y guardar cambios
                _context.Add(tarea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            

            // Si el modelo no es válido, mostramos la vista nuevamente con los datos
            ViewData["IdProyecto"] = new SelectList(_context.Proyectos, "IdProyecto", "Nombre", tarea.IdProyecto);
            return View(tarea);
        }


        // GET: Tareas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }

            ViewData["IdProyecto"] = new SelectList(_context.Proyectos, "IdProyecto", "IdProyecto", tarea.IdProyecto);
            return View(tarea);
        }

        // POST: Tareas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTarea,IdProyecto,Nombre,Descripcion,TiempoRealizado,TiempoEsperado,FechaCreacion,Estado")] Tarea tarea)
        {
            if (id != tarea.IdTarea)
            {
                return NotFound();
            }

           
            
                try
                {
                    // Verifica que 'Estado' tenga un valor válido
                    if (string.IsNullOrWhiteSpace(tarea.Estado))
                    {
                        tarea.Estado = "Pendiente"; // Valor por defecto
                    }

                    _context.Update(tarea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TareaExists(tarea.IdTarea))
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
        // GET: Tareas/Reporte
        public async Task<IActionResult> Reporte()
        {
            // Obtener las tareas y sus relaciones con Proyecto y Participantes
            var tareasReporte = await _context.Tareas
                .Include(t => t.IdProyectoNavigation) // Incluimos el proyecto relacionado
                .Include(t => t.IdParticipantes)      // Incluimos los participantes asignados
                .ToListAsync();

            return View(tareasReporte);
        }
        public async Task<IActionResult> MarkTaskAsCompleted(int taskId)
        {
            var task = await _context.Tareas.FindAsync(taskId);
            if (task != null)
            {
                task.Estado = "Finalizado"; // O el valor que uses para tarea finalizada
                await _context.SaveChangesAsync();

                // Calcular las estadísticas actualizadas
                var totalTareas = await _context.Tareas.CountAsync();
                var tareasCompletadas = await _context.Tareas.CountAsync(t => t.Estado == "Finalizado");
                var tareasPendientes = totalTareas - tareasCompletadas;

                var statistics = new
                {
                    TareasCompletadas = tareasCompletadas,
                    TareasPendientes = tareasPendientes
                };

                return Json(statistics); // Devolver las estadísticas actualizadas en formato JSON
            }

            return BadRequest();
        }
        // Método para generar PDF
        public async Task<IActionResult> CrearPdf()
        {
            // Obtener las tareas con los datos necesarios
            var tareasReporte = await _context.Tareas.Include(t => t.IdProyectoNavigation)
                                                      .Include(t => t.IdParticipantes)
                                                      .ToListAsync();

            // Crear un documento PDF
            using (var memoryStream = new System.IO.MemoryStream())
            {
                PdfDocument pdf = new PdfDocument();
                PdfPage page = pdf.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Fuente para el texto
                XFont font = new XFont("Arial", 12);

                // Agregar título
                gfx.DrawString("Reporte de Tareas", new XFont("Arial", 20), XBrushes.Black, new XRect(0, 20, page.Width, 40), XStringFormats.Center);

                // Agregar encabezados de las columnas
                int yPos = 60;
                gfx.DrawString("Nombre de la Tarea", font, XBrushes.Black, new XRect(40, yPos, page.Width, 40), XStringFormats.TopLeft);
                gfx.DrawString("Nombre del Proyecto", font, XBrushes.Black, new XRect(200, yPos, page.Width, 40), XStringFormats.TopLeft);
                gfx.DrawString("Participante(s)", font, XBrushes.Black, new XRect(400, yPos, page.Width, 40), XStringFormats.TopLeft);
                gfx.DrawString("Tiempo Esperado", font, XBrushes.Black, new XRect(600, yPos, page.Width, 40), XStringFormats.TopLeft);

                yPos += 20;

                // Llenar las filas con los datos de las tareas
                foreach (var tarea in tareasReporte)
                {
                    gfx.DrawString(tarea.Nombre, font, XBrushes.Black, new XRect(40, yPos, page.Width, 40), XStringFormats.TopLeft);
                    gfx.DrawString(tarea.IdProyectoNavigation.Nombre, font, XBrushes.Black, new XRect(200, yPos, page.Width, 40), XStringFormats.TopLeft);
                    gfx.DrawString(string.Join(", ", tarea.IdParticipantes.Select(p => p.Nombre)), font, XBrushes.Black, new XRect(400, yPos, page.Width, 40), XStringFormats.TopLeft);
                    gfx.DrawString(tarea.TiempoEsperado.ToString(), font, XBrushes.Black, new XRect(600, yPos, page.Width, 40), XStringFormats.TopLeft);

                    yPos += 20;
                }

                // Guardar el archivo en memoria
                pdf.Save(memoryStream, false);

                // Devolver el archivo PDF al usuario
                return File(memoryStream.ToArray(), "application/pdf", "reporte_tareas.pdf");
            }
        }

        // GET: Tareas
        public async Task<IActionResult> Index()
        {
            var tareas = _context.Tareas.Include(t => t.IdProyectoNavigation);
            return View(await tareas.ToListAsync());
        }

        private bool TareaExists(int id)
        {
            return _context.Tareas.Any(e => e.IdTarea == id);
        }
    }
}
