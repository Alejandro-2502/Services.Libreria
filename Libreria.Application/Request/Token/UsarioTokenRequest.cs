using System.ComponentModel.DataAnnotations;

namespace Libreria.Application.Request.Token;

public class UsarioTokenRequest
{
    [Required]
    public string User { get; set; }
    [Required]
    public string Password { get; set; }
}
