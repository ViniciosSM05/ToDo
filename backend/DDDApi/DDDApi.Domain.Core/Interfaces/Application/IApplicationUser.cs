using DDDApi.Domain.Core.DTO.User;

namespace DDDApi.Domain.Core.Interfaces.Application
{
    public interface IApplicationUser
    {
        Task<UserSaveResponseDTO> Save(UserSaveDTO obj, CancellationToken cancellationToken);
        Task<UserLoginResponseDTO> LoginAsync(UserLoginDTO obj, CancellationToken cancellationToken);
    }
}
