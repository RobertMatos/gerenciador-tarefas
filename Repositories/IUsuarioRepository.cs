using System.Collections.Generic;
using TarefasApi.Models;

namespace TarefasApi.Repositories
{
    public interface IUsuarioRepository
    {
        Usuario ObterUsuarioPorId(int id);
        List<Usuario> ObterTodosUsuarios();
        void CriarUsuario(Usuario usuario);
        
        Usuario? ObterPorEmail(string email);
    }
}