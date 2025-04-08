using TarefasApi.DTOs;
using TarefasApi.Models;

namespace TarefasApi.Services;

public interface IUsuarioService
{
    Usuario CriarUsuario(UsuarioCreateDTO usuarioDTO);
    Usuario ObterUsuarioPorId(int id);
    List<Usuario> ObterTodosUsuarios(int pagina = 1, int tamanhoPagina = 10);
    Usuario? ObterPorEmail(string email);

}
