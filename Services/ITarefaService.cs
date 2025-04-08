using TarefasApi.DTOs;
using TarefasApi.Models;

namespace TarefasApi.Services;

public interface ITarefaService
{
    Tarefa CriarTarefa(int usuarioId, TarefaCreateDTO tarefaDTO);
    List<Tarefa> ListarTarefasPorUsuario(int usuarioId, int pagina = 1, int tamanhoPagina = 10);
}
