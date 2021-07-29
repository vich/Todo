using AutoMapper;

namespace Todo.Api.Profiles
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<Entities.TodoItem, Models.TodoItemDTO>();
            CreateMap<Models.TodoItemDTO, Entities.TodoItem>();
        }
    }
}
