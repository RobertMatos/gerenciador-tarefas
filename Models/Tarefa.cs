namespace TarefasApi.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public StatusTarefa Status { get; set; } = StatusTarefa.Pendente;
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}