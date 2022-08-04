using AutoMapper;
using DDDApi.Domain.Core.DTO.Todo;
using DDDApi.Domain.Core.Entities;

namespace DDDApi.Domain.Mappers
{
    public class MapperTodo : Profile
    {
        public MapperTodo()
        {
            CreateMap<TodoSaveDTO, Todo>().ReverseMap();
            CreateMap<Todo, TodoSaveResponseDTO>();
        }
    }
}
