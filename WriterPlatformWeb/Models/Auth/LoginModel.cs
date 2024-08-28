using System.ComponentModel.DataAnnotations;

namespace WriterPlatformWeb.Models.Auth;

public class LoginModel
{
    [Required(ErrorMessage = "Не указан логин или email")]
    public string LoginOrEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}