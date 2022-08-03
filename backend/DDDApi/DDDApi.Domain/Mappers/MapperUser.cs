using AutoMapper;
using DDDApi.Domain.Core.DTO.User;
using DDDApi.Domain.Core.Entities;

namespace DDDApi.Domain.Mappers
{
    public class MapperUser : Profile
    {
        public MapperUser()
        {
            CreateMap<UserSaveDTO, User>().ReverseMap();
            CreateMap<User, UserSaveResponseDTO>();
            CreateMap<User, UserTokenDTO>();
            CreateMap<User, UserLoginResponseDTO>();
        }

    }
}
