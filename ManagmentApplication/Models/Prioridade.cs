using System;
using System.Collections.Generic;

namespace ManagmentApplication.Models;

public partial class Prioridade
{
    public int IdPrioridad { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Tarea> IdTareas { get; set; } = new List<Tarea>();
}
