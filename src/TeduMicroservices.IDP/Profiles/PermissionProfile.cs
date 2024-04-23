using AutoMapper;
using TeduMicroservices.IDP.Infrastructure.Entities;
using TeduMicroservices.IDP.Infrastructure.ViewModels;

namespace TeduMicroservices.IDP.Profiles;

public class PermissionProfile : Profile
{
    public PermissionProfile()
    {
        CreateMap<Permission, PermissionUserViewModel>();
    }
}
