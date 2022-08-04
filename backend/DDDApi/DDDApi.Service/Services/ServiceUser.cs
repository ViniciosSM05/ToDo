using AutoMapper;
using DDDApi.Domain.Core.Interfaces.Notification;
using DDDApi.Domain.Core.Interfaces.Repository;
using DDDApi.Domain.Core.Interfaces.Service;
using DDDApi.Domain.Core.DTO.User;
using DDDApi.Domain.Core.Entities;
using DDDApi.Infra.CrossCutting.Security;
using FluentValidation;
using DDDApi.Domain.Core.Interfaces.Auth;

namespace DDDApi.Service.Services
{
    public class ServiceUser : IServiceUser
    {
        private readonly IRepositoryUser repositoryUser;
        private readonly IMapper mapper;
        private readonly INotification notification;
        private readonly IValidator<UserSaveDTO> validator;
        private readonly IAuthToken authToken;
        public ServiceUser(
            IRepositoryUser repositoryUser, 
            IMapper mapper, 
            INotification notification,
            IValidator<UserSaveDTO> validator,
            IAuthToken authToken)
        {
            this.repositoryUser = repositoryUser;
            this.mapper = mapper;
            this.notification = notification;
            this.validator = validator;
            this.authToken = authToken;
        }

        public async Task<UserSaveResponseDTO> SaveAsync(UserSaveDTO obj, CancellationToken cancellationToken)
        {
            notification.AddFieldMessages(await validator.ValidateAsync(obj, cancellationToken));    
            if (!notification.IsValid) return null;

            var model = mapper.Map<User>(obj);
            model.Password = Cryptography.Execute(obj.Password);

            await repositoryUser.AddAsync(model, cancellationToken);
            return mapper.Map<UserSaveResponseDTO>(model);
        }

        public async Task<UserLoginResponseDTO> LoginAsync(UserLoginDTO obj, CancellationToken cancellationToken)
        {
            var user = await repositoryUser.GetByCredentialsAsync(obj.Email, obj.Password, cancellationToken);
            if (user is null) { notification.AddMessage("Usuário não encontrado"); return null; }

            var response = mapper.Map<UserLoginResponseDTO>(user);
            response.Token = authToken.GenerateToken(mapper.Map<UserTokenDTO>(user));

            return response;
        }
    }
}
