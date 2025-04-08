using System.ComponentModel.DataAnnotations;

namespace TarefasApi.DTOs
{
    public class TarefaCreateDTO
    {
        [Required(ErrorMessage = "O título é obrigatório")]
        [MinLength(3, ErrorMessage = "O título deve ter pelo menos 3 caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [MinLength(5, ErrorMessage = "A descrição deve ter pelo menos 5 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O ID do usuário é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O ID do usuário deve ser maior que zero")]
        public int UsuarioId { get; set; }
    }
}