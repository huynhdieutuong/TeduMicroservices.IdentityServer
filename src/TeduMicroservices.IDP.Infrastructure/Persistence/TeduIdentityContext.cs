﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TeduMicroservices.IDP.Infrastructure.Entities;
using TeduMicroservices.IDP.Infrastructure.Extensions;

namespace TeduMicroservices.IDP.Infrastructure.Persistence;

public class TeduIdentityContext : IdentityDbContext<User>
{
    public IDbConnection Connection => Database.GetDbConnection();

    public TeduIdentityContext(DbContextOptions<TeduIdentityContext> options) : base(options)
    {
    }

    public DbSet<Permission> Permissions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(TeduIdentityContext).Assembly);
        builder.ApplyIdentityConfiguration();
    }
}
