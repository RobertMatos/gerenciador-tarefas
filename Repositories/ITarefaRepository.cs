using System.Collections.Generic;
using TarefasApi.Models;

namespace TarefasApi.Repositories
{
    public interface ITarefaRepository
    {
        List<Tarefa> ObterTarefasPorUsuario(int usuarioId);
        void AdicionarTarefa(Tarefa tarefa);
    }
}