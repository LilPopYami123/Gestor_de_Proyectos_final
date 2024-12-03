using System;
using System.Collections.Generic;
using ManagmentApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagmentApplication.Data;

public partial class MiContexto : DbContext
{
    public MiContexto()
    {
    }

    public MiContexto(DbContextOptions<MiContexto> options)
        : base(options)
    {
    }

    public virtual DbSet<ArchivosAdjunto> ArchivosAdjuntos { get; set; }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<HistorialCambio> HistorialCambios { get; set; }

    public virtual DbSet<Participante> Participantes { get; set; }

    public virtual DbSet<Prioridade> Prioridades { get; set; }

    public virtual DbSet<Proyecto> Proyectos { get; set; }

    public virtual DbSet<StatusProyecto> StatusProyectos { get; set; }

    public virtual DbSet<Tarea> Tareas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-42JI4AC\\SQLEXPRESS;Database=ManagmentDatabase;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArchivosAdjunto>(entity =>
        {
            entity.HasKey(e => e.IdArchivo).HasName("PK__Archivos__26B92111B803CA4E");

            entity.Property(e => e.FechaSubida)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreArchivo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RutaArchivo)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTareaNavigation).WithMany(p => p.ArchivosAdjuntos)
                .HasForeignKey(d => d.IdTarea)
                .HasConstraintName("FK__ArchivosA__IdTar__628FA481");
        });

        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.IdComentario).HasName("PK__Comentar__DDBEFBF95DFEA25A");

            entity.Property(e => e.Comentario1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Comentario");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdTareaNavigation).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.IdTarea)
                .HasConstraintName("FK__Comentari__IdTar__5EBF139D");
        });

        modelBuilder.Entity<HistorialCambio>(entity =>
        {
            entity.HasKey(e => e.IdCambio).HasName("PK__Historia__B4BBA86E9882E207");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.HistorialCambios)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK__Historial__IdPro__66603565");
        });

        modelBuilder.Entity<Participante>(entity =>
        {
            entity.HasKey(e => e.IdParticipante).HasName("PK__Particip__561392428C072274");

            entity.HasIndex(e => e.Correo, "UQ__Particip__60695A194FEDC0A9").IsUnique();

            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasMany(d => d.IdTareas).WithMany(p => p.IdParticipantes)
                .UsingEntity<Dictionary<string, object>>(
                    "ParticipantesTarea",
                    r => r.HasOne<Tarea>().WithMany()
                        .HasForeignKey("IdTarea")
                        .HasConstraintName("FK__Participa__IdTar__59063A47"),
                    l => l.HasOne<Participante>().WithMany()
                        .HasForeignKey("IdParticipante")
                        .HasConstraintName("FK__Participa__IdPar__5812160E"),
                    j =>
                    {
                        j.HasKey("IdParticipante", "IdTarea").HasName("PK__Particip__C8BE7B4B227558C8");
                        j.ToTable("ParticipantesTareas");
                    });
        });

        modelBuilder.Entity<Prioridade>(entity =>
        {
            entity.HasKey(e => e.IdPrioridad).HasName("PK__Priorida__0FC70DD54B1183DF");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.IdProyecto).HasName("PK__Proyecto__F48886738FAA9485");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Pendiente");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaFinEstimada).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StatusProyecto>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("PK__StatusPr__B450643A2D3399BD");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.IdTarea).HasName("PK__Tareas__EADE909801234857");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Pendiente");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TiempoEsperado).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TiempoRealizado)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK__Tareas__IdProyec__52593CB8");

            entity.HasMany(d => d.IdPrioridads).WithMany(p => p.IdTareas)
                .UsingEntity<Dictionary<string, object>>(
                    "TareasPrioridade",
                    r => r.HasOne<Prioridade>().WithMany()
                        .HasForeignKey("IdPrioridad")
                        .HasConstraintName("FK__TareasPri__IdPri__6C190EBB"),
                    l => l.HasOne<Tarea>().WithMany()
                        .HasForeignKey("IdTarea")
                        .HasConstraintName("FK__TareasPri__IdTar__6B24EA82"),
                    j =>
                    {
                        j.HasKey("IdTarea", "IdPrioridad").HasName("PK__TareasPr__AA22E0453ADEE865");
                        j.ToTable("TareasPrioridades");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
