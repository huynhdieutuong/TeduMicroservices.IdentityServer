﻿namespace TeduMicroservices.IDP.Infrastructure.Domains.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<int> CommitAsync();
}
