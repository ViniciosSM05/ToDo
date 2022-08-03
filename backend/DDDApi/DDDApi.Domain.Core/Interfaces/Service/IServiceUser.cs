using DDDApi.Domain.Core.DTO.User;

namespace DDDApi.Domain.Core.Interfaces.Service
{
    public interface IServiceUser
    {
        Task<UserSaveResponseDTO> Save(UserSaveDTO obj, CancellationToken cancellationToken);
        Task<UserLoginResponseDTO> Login(UserLoginDTO obj, CancellationToken cancellationToken);
    }
}
