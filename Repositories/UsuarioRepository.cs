using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TarefasApi.Data;
using TarefasApi.Models;

namespace TarefasApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public Usuario ObterUsuarioPorId(int id)
        {
            return _context.Usuarios.AsNoTracking().FirstOrDefault(u => u.Id == id);
        }

        public List<Usuario> ObterTodosUsuarios()
        {
            return _context.Usuarios.AsNoTracking().ToList();
        }

        public void CriarUsuario(Usuario usuario)
        {
            try
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar usuário.", ex);
            }
        }
        
        public Usuario? ObterPorEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email);
        }

    }
}