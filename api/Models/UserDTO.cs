using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class UserDTO
{
    [Required(ErrorMessage = "Erro: nome não informado.")]
    [MaxLength(50, ErrorMessage = "Erro: nome maior que 50 caracteres.")]

    public string Username { get; set; } = string.Empty;
    

    [Required(ErrorMessage = "Erro: senha não informada.")]
    public string Password { get; set; } = string.Empty;
}