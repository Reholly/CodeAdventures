using AutoMapper;
using Data.Entities;
using Shared.DTO;

namespace Server.Mapping;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Article, ArticleModel>();
        CreateMap<User, UserModel>();
        CreateMap<User, RegisterModel>();
        CreateMap<User, LoginModel>();
        CreateMap<UserModel, User>();
        CreateMap<RegisterModel, User>();
        CreateMap<LoginModel, User>();
        CreateMap<Tiding, TidingModel>();
        CreateMap<TidingModel, Tiding>();
    }
}