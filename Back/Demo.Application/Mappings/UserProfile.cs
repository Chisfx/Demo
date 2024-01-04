using AutoMapper;
using Demo.Application.DTOs;
using Demo.Application.Features.Commands;
using Demo.Domain.Entities;
namespace Demo.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserCommand, User>().ReverseMap();
            CreateMap<CreateUserCommand, UserModel>().ReverseMap();
            CreateMap<UpdateUserCommand, User>().ReverseMap();
            CreateMap<UpdateUserCommand, UserModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
        }
    }
}
