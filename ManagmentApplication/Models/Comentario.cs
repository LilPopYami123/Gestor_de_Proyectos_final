using System;
using System.Collections.Generic;

namespace ManagmentApplication.Models;

public partial class Comentario
{
    public int IdComentario { get; set; }

    public int IdTarea { get; set; }

    public string Comentario1 { get; set; } = null!;

    public DateTime? Fecha { get; set; }

    public virtual Tarea IdTareaNavigation { get; set; } = null!;
}
