using System;
using System.Collections.Generic;

namespace ManagmentApplication.Models;

public partial class StatusProyecto
{
    public int IdStatus { get; set; }

    public string Descripcion { get; set; } = null!;
}
