namespace ManagmentApplication.Models
{
    public class ParticipanteTarea
    {
        public int IdParticipante { get; set; }
        public Participante Participante { get; set; } = null!;

        public int IdTarea { get; set; }
        public Tarea Tarea { get; set; } = null!;
    }

}
