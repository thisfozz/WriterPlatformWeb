using System.ComponentModel.DataAnnotations;

namespace WriterPlatformWeb.Models.Auth;

public class LoginModel
{
    [Required(ErrorMessage = "Не указан логин или email")]
    [Display(Name = "Логин или электронная почта")]
    public string LoginOrEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; } = string.Empty;
}