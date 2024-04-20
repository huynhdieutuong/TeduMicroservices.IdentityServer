using System.ComponentModel.DataAnnotations;

namespace TeduMicroservices.IDP.Infrastructure.ViewModels;
public class PermissionAddModel
{
    [Required]
    public required string Function { get; set; }

    [Required]
    public required string Command { get; set; }
}
