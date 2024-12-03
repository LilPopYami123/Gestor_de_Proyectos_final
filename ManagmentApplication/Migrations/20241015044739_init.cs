using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagmentApplication.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Participantes",
                columns: table => new
                {
                    IdParticipante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Correo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Particip__561392428C072274", x => x.IdParticipante);
                });

            migrationBuilder.CreateTable(
                name: "Prioridades",
                columns: table => new
                {
                    IdPrioridad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Priorida__0FC70DD54B1183DF", x => x.IdPrioridad);
                });

            migrationBuilder.CreateTable(
                name: "Proyectos",
                columns: table => new
                {
                    IdProyecto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    FechaFinEstimada = table.Column<DateTime>(type: "datetime", nullable: true),
                    Estado = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "Pendiente")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Proyecto__F48886738FAA9485", x => x.IdProyecto);
                });

            migrationBuilder.CreateTable(
                name: "StatusProyectos",
                columns: table => new
                {
                    IdStatus = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StatusPr__B450643A2D3399BD", x => x.IdStatus);
                });

            migrationBuilder.CreateTable(
                name: "HistorialCambios",
                columns: table => new
                {
                    IdCambio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProyecto = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Historia__B4BBA86E9882E207", x => x.IdCambio);
                    table.ForeignKey(
                        name: "FK__Historial__IdPro__66603565",
                        column: x => x.IdProyecto,
                        principalTable: "Proyectos",
                        principalColumn: "IdProyecto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tareas",
                columns: table => new
                {
                    IdTarea = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProyecto = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TiempoRealizado = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0m),
                    TiempoEsperado = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Estado = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "Pendiente")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tareas__EADE909801234857", x => x.IdTarea);
                    table.ForeignKey(
                        name: "FK__Tareas__IdProyec__52593CB8",
                        column: x => x.IdProyecto,
                        principalTable: "Proyectos",
                        principalColumn: "IdProyecto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArchivosAdjuntos",
                columns: table => new
                {
                    IdArchivo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTarea = table.Column<int>(type: "int", nullable: false),
                    NombreArchivo = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    RutaArchivo = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    FechaSubida = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Archivos__26B92111B803CA4E", x => x.IdArchivo);
                    table.ForeignKey(
                        name: "FK__ArchivosA__IdTar__628FA481",
                        column: x => x.IdTarea,
                        principalTable: "Tareas",
                        principalColumn: "IdTarea",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    IdComentario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTarea = table.Column<int>(type: "int", nullable: false),
                    Comentario = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Comentar__DDBEFBF95DFEA25A", x => x.IdComentario);
                    table.ForeignKey(
                        name: "FK__Comentari__IdTar__5EBF139D",
                        column: x => x.IdTarea,
                        principalTable: "Tareas",
                        principalColumn: "IdTarea",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantesTareas",
                columns: table => new
                {
                    IdParticipante = table.Column<int>(type: "int", nullable: false),
                    IdTarea = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Particip__C8BE7B4B227558C8", x => new { x.IdParticipante, x.IdTarea });
                    table.ForeignKey(
                        name: "FK__Participa__IdPar__5812160E",
                        column: x => x.IdParticipante,
                        principalTable: "Participantes",
                        principalColumn: "IdParticipante",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Participa__IdTar__59063A47",
                        column: x => x.IdTarea,
                        principalTable: "Tareas",
                        principalColumn: "IdTarea",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TareasPrioridades",
                columns: table => new
                {
                    IdTarea = table.Column<int>(type: "int", nullable: false),
                    IdPrioridad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TareasPr__AA22E0453ADEE865", x => new { x.IdTarea, x.IdPrioridad });
                    table.ForeignKey(
                        name: "FK__TareasPri__IdPri__6C190EBB",
                        column: x => x.IdPrioridad,
                        principalTable: "Prioridades",
                        principalColumn: "IdPrioridad",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__TareasPri__IdTar__6B24EA82",
                        column: x => x.IdTarea,
                        principalTable: "Tareas",
                        principalColumn: "IdTarea",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchivosAdjuntos_IdTarea",
                table: "ArchivosAdjuntos",
                column: "IdTarea");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_IdTarea",
                table: "Comentarios",
                column: "IdTarea");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialCambios_IdProyecto",
                table: "HistorialCambios",
                column: "IdProyecto");

            migrationBuilder.CreateIndex(
                name: "UQ__Particip__60695A194FEDC0A9",
                table: "Participantes",
                column: "Correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantesTareas_IdTarea",
                table: "ParticipantesTareas",
                column: "IdTarea");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_IdProyecto",
                table: "Tareas",
                column: "IdProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_TareasPrioridades_IdPrioridad",
                table: "TareasPrioridades",
                column: "IdPrioridad");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchivosAdjuntos");

            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "HistorialCambios");

            migrationBuilder.DropTable(
                name: "ParticipantesTareas");

            migrationBuilder.DropTable(
                name: "StatusProyectos");

            migrationBuilder.DropTable(
                name: "TareasPrioridades");

            migrationBuilder.DropTable(
                name: "Participantes");

            migrationBuilder.DropTable(
                name: "Prioridades");

            migrationBuilder.DropTable(
                name: "Tareas");

            migrationBuilder.DropTable(
                name: "Proyectos");
        }
    }
}
