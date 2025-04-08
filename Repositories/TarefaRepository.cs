using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TarefasApi.Data;
using TarefasApi.Models;

namespace TarefasApi.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly AppDbContext _context;

        public TarefaRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Tarefa> ObterTarefasPorUsuario(int usuarioId)
        {
            return _context.Tarefas
                .AsNoTracking()
                .Where(t => t.UsuarioId == usuarioId)
                .ToList();
        }

        public void AdicionarTarefa(Tarefa tarefa)
        {
            var usuarioExiste = _context.Usuarios
                .AsNoTracking()
                .Any(u => u.Id == tarefa.UsuarioId);

            if (!usuarioExiste)
                throw new Exception("Usuário não encontrado.");

            try
            {
                _context.Tarefas.Add(tarefa);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar a tarefa no banco de dados.", ex);
            }
        }
    }
}