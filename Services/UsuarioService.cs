using TarefasApi.DTOs;
using TarefasApi.Models;
using TarefasApi.Repositories;

namespace TarefasApi.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
    
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
    
        public Usuario CriarUsuario(UsuarioCreateDTO usuarioDTO)
        {
            if (string.IsNullOrWhiteSpace(usuarioDTO.Nome) || string.IsNullOrWhiteSpace(usuarioDTO.Email) || string.IsNullOrWhiteSpace(usuarioDTO.Senha))
            {
                throw new ArgumentException("Nome, Email e Senha são obrigatórios.");
            }

            var usuarioExistente = _usuarioRepository.ObterTodosUsuarios()
                .FirstOrDefault(u => u.Email == usuarioDTO.Email);

            if (usuarioExistente != null)
            {
                throw new ArgumentException("Já existe um usuário com esse email.");
            }

            var usuario = new Usuario
            {
                Nome = usuarioDTO.Nome,
                Email = usuarioDTO.Email,
                Senha = BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Senha)
            };

            _usuarioRepository.CriarUsuario(usuario);
            return usuario;
        }
    
        public Usuario ObterUsuarioPorId(int id)
        {
            return _usuarioRepository.ObterUsuarioPorId(id);
        }
    
        public List<Usuario> ObterTodosUsuarios(int pagina = 1, int tamanhoPagina = 10)
        {
            if (pagina < 1) pagina = 1;

            return _usuarioRepository.ObterTodosUsuarios()
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToList();
        }
        
        public Usuario? ObterPorEmail(string email)
        {
            return _usuarioRepository.ObterTodosUsuarios()
                .FirstOrDefault(u => u.Email == email);
        }
    }
}