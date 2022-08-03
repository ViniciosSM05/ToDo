using DDDApi.Domain.Core.DTO.User;

namespace DDDApi.Domain.Core.Interfaces.Auth
{
    public interface IAuthToken
    {
        string GenerateToken(UserTokenDTO user);
    }
}
