using TeduMicroservices.IDP.Infrastructure.Domains;
using TeduMicroservices.IDP.Infrastructure.Domains.Interfaces;
using TeduMicroservices.IDP.Infrastructure.Entities;
using TeduMicroservices.IDP.Infrastructure.Persistence;
using TeduMicroservices.IDP.Infrastructure.Repositories.Interfaces;
using TeduMicroservices.IDP.Infrastructure.ViewModels;

namespace TeduMicroservices.IDP.Infrastructure.Repositories;

public class PermissionRepository : RepositoryBase<Permission, long>, IPermissionRepository
{
    public PermissionRepository(TeduIdentityContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
    {
    }

    public Task<IReadOnlyList<PermissionViewModel>> GetPermissionsByRole(string roleId)
    {
        throw new NotImplementedException();
    }

    public Task<PermissionViewModel> CreatePermission(string roleId, PermissionAddModel model)
    {
        throw new NotImplementedException();
    }

    public Task DeletePermission(string roleId, string function, string command)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePermissionsByRoleId(string roleId, IEnumerable<PermissionAddModel> permissionCollection)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PermissionUserViewModel>> GetPermissionsByUser(User user)
    {
        throw new NotImplementedException();
    }
}
