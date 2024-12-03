using System;
using System.Collections.Generic;

namespace ManagmentApplication.Models;

public partial class Participante
{
    public int IdParticipante { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string? Telefono { get; set; }

    public virtual ICollection<Tarea> IdTareas { get; set; } = new List<Tarea>();
}
