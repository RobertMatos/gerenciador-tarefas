using Microsoft.AspNetCore.Mvc;
using TarefasApi.DTOs;
using TarefasApi.Services;

namespace TarefasApi.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ITarefaService _tarefaService;

        public UsuarioController(IUsuarioService usuarioService, ITarefaService tarefaService)
        {
            _usuarioService = usuarioService;
            _tarefaService = tarefaService;
        }

        [HttpPost]
        public IActionResult CriarUsuario([FromBody] UsuarioCreateDTO usuarioDTO)
        {
            if (_usuarioService.ObterPorEmail(usuarioDTO.Email) != null)
            {
                return BadRequest("E-mail já cadastrado.");
            }

            _usuarioService.CriarUsuario(usuarioDTO);

            return Ok("Usuário registrado com sucesso.");
        }

        [HttpGet("{id}")]
        public IActionResult GetUsuarioPorId(int id)
        {
            var usuario = _usuarioService.ObterUsuarioPorId(id);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpGet]
        public IActionResult GetUsuarios([FromQuery] int pagina = 1, [FromQuery] int tamanhoPagina = 10)
        {
            var usuarios = _usuarioService.ObterTodosUsuarios(pagina, tamanhoPagina);
            return Ok(usuarios);
        }

        [HttpPost("{id}/tarefas")]
        public IActionResult CriarTarefaParaUsuario(int id, [FromBody] TarefaCreateDTO tarefaDTO)
        {
            try
            {
                var tarefa = _tarefaService.CriarTarefa(id, tarefaDTO);
                return CreatedAtAction(nameof(ListarTarefasPorUsuario), new { id }, tarefa);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor.");
            }
        }

        [HttpGet("{id}/tarefas")]
        public IActionResult ListarTarefasPorUsuario(int id, [FromQuery] int pagina = 1, [FromQuery] int tamanhoPagina = 10)
        {
            try
            {
                var tarefas = _tarefaService.ListarTarefasPorUsuario(id, pagina, tamanhoPagina);
                return Ok(tarefas);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor.");
            }
        }
    }
}