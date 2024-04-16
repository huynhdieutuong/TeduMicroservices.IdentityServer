namespace TeduMicroservices.IDP.Infrastructure.Domains.Interfaces;

public interface IEntityBase<T>
{
    T Id { get; set; }
}
