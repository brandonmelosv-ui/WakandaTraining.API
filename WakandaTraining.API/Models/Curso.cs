namespace WakandaTraining.API.Models
{
    public class Curso
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        public int CargaHoraria { get; set; }
        public bool Concluido { get; set; }
    }
}