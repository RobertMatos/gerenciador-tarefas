using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TarefasApi.DTOs;
using TarefasApi.Models;
using TarefasApi.Repositories;

namespace TarefasApi.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public TarefaService(ITarefaRepository tarefaRepository, IUsuarioRepository usuarioRepository)
        {
            _tarefaRepository = tarefaRepository;
            _usuarioRepository = usuarioRepository;
        }

        public Tarefa CriarTarefa(int usuarioId, TarefaCreateDTO tarefaDTO)
        {
            var usuario = _usuarioRepository.ObterUsuarioPorId(usuarioId);
            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            var tarefa = new Tarefa
            {
                Titulo = tarefaDTO.Titulo,
                Descricao = tarefaDTO.Descricao,
                UsuarioId = usuarioId
            };

            _tarefaRepository.AdicionarTarefa(tarefa);
            return tarefa;
        }

        public List<Tarefa> ListarTarefasPorUsuario(int usuarioId, int pagina = 1, int tamanhoPagina = 10)
        {
            if (pagina < 1) pagina = 1;
            
            var usuario = _usuarioRepository.ObterUsuarioPorId(usuarioId);
            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            return _tarefaRepository.ObterTarefasPorUsuario(usuarioId)
                .AsQueryable()
                .Skip((pagina - 1) * tamanhoPagina) 
                .Take(tamanhoPagina) 
                .AsNoTracking() 
                .ToList();

        }
    }
}