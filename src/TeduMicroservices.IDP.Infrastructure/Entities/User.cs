﻿using Microsoft.AspNetCore.Identity;

namespace TeduMicroservices.IDP.Infrastructure.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}
