﻿namespace WriterPlatformWeb.Models.ViewModel.User;

public class UserViewModel
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public bool? IsDeleted { get; set; }
}