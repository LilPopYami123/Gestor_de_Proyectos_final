using System;
using System.Collections.Generic;

namespace ManagmentApplication.Models;

public partial class ArchivosAdjunto
{
    public int IdArchivo { get; set; }

    public int IdTarea { get; set; }

    public string NombreArchivo { get; set; } = null!;

    public string RutaArchivo { get; set; } = null!;

    public DateTime? FechaSubida { get; set; }

    public virtual Tarea IdTareaNavigation { get; set; } = null!;
}
