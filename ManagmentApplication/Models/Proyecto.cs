using System;
using System.Collections.Generic;

namespace ManagmentApplication.Models;

public partial class Proyecto
{
    public int IdProyecto { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaFinEstimada { get; set; }

    public string? Estado { get; set; }

    public string ImagenUrl { get; set; }
    public virtual ICollection<HistorialCambio> HistorialCambios { get; set; } = new List<HistorialCambio>();

    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();

}
