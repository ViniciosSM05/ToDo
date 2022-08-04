using DDDApi.Domain.Core.DTO.User;

namespace DDDApi.Domain.Core.Interfaces.Service
{
    public interface IServiceUser
    {
        Task<UserSaveResponseDTO> SaveAsync(UserSaveDTO obj, CancellationToken cancellationToken);
        Task<UserLoginResponseDTO> LoginAsync(UserLoginDTO obj, CancellationToken cancellationToken);
    }
}
