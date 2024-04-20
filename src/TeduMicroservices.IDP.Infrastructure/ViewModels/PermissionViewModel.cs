﻿using TeduMicroservices.IDP.Infrastructure.Domains;

namespace TeduMicroservices.IDP.Infrastructure.ViewModels;

public class PermissionViewModel : EntityBase<long>
{
    public string Function { get; set; } = string.Empty;

    public string Command { get; set; } = string.Empty;

    public string RoleId { get; set; } = string.Empty;
}
