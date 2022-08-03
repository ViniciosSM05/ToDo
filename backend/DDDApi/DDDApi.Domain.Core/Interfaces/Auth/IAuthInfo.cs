namespace DDDApi.Domain.Core.Interfaces.Auth
{
    public interface IAuthInfo
    {
        Guid Id { get; }
        string Name { get; }
        string Email { get; }
    }
}
