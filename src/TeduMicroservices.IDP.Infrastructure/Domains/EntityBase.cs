using TeduMicroservices.IDP.Infrastructure.Domains.Interfaces;

namespace TeduMicroservices.IDP.Infrastructure.Domains;

public abstract class EntityBase<TKey> : IEntityBase<TKey>
{
    public TKey Id { get; set; }
}
