using System;
using System.Collections.Generic;

namespace ManagmentApplication.Models;

public partial class Tarea
{
    public int IdTarea { get; set; }

    public int IdProyecto { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal? TiempoRealizado { get; set; }

    public decimal TiempoEsperado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<ArchivosAdjunto> ArchivosAdjuntos { get; set; } = new List<ArchivosAdjunto>();

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;

    public virtual ICollection<Participante> IdParticipantes { get; set; } = new List<Participante>();

    public virtual ICollection<Prioridade> IdPrioridads { get; set; } = new List<Prioridade>();
}
