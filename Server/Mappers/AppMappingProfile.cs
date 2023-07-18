using AutoMapper;
using Data.Entities;
using Shared.DTO;

namespace Server.Mappers;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Article, ArticleModel>();
    }
}