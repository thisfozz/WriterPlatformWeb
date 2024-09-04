using System.ComponentModel.DataAnnotations;

namespace WriterPlatformWeb.Models.Auth;

public class RegisterModel
{
    [Required(ErrorMessage = "Не указан логин")]
    [MinLength(3, ErrorMessage = "Логин должен содержать не менее 3 символов")]
    [Display(Name = "Логин")]
    public string Login { get; set; } = string.Empty;

    [Required(ErrorMessage = "Не указан Email")]
    [EmailAddress(ErrorMessage = "Неверный формат Email")]
    [Display(Name = "Электронная почта")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "Пароль должен быть не менее 6 символов")]
    [Display(Name = "Пароль")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Подтвердите пароль")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [Display(Name = "Подтверждение пароля")]
    public string ConfirmPassword { get; set; } = string.Empty;
}