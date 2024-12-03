using System;
using System.Collections.Generic;

namespace ManagmentApplication.Models;

public partial class HistorialCambio
{
    public int IdCambio { get; set; }

    public int IdProyecto { get; set; }

    public string Descripcion { get; set; } = null!;

    public DateTime? Fecha { get; set; }

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
}
