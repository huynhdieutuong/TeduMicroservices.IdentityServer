using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using TeduMicroservices.IDP.Infrastructure.Domains.Interfaces;
using TeduMicroservices.IDP.Infrastructure.Entities;
using TeduMicroservices.IDP.Infrastructure.Persistence;
using TeduMicroservices.IDP.Infrastructure.Repositories.Interfaces;

namespace TeduMicroservices.IDP.Infrastructure.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly TeduIdentityContext _dbContext;
    private readonly Lazy<IPermissionRepository> _permissionRepository;
    private readonly IMapper _mapper;

    public RepositoryManager(IUnitOfWork unitOfWork,
                             TeduIdentityContext dbContext,
                             UserManager<User> userManager,
                             RoleManager<IdentityRole> roleManager,
                             IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _dbContext = dbContext;
        UserManager = userManager;
        RoleManager = roleManager;
        _mapper = mapper;
        _permissionRepository = new Lazy<IPermissionRepository>(() => new PermissionRepository(_dbContext, _unitOfWork, UserManager, _mapper));
    }

    public UserManager<User> UserManager { get; }
    public RoleManager<IdentityRole> RoleManager { get; }
    public IPermissionRepository Permission => _permissionRepository.Value;

    public Task<int> SaveAsync() => _unitOfWork.CommitAsync();

    public Task<IDbContextTransaction> BeginTransactionAsync() => _dbContext.Database.BeginTransactionAsync();

    public Task EndTransactionAsync() => _dbContext.Database.CommitTransactionAsync();

    public void RollbackTransaction() => _dbContext.Database.RollbackTransactionAsync();
}
