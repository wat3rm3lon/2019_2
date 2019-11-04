using AutoMapper;
using EntityApi.Dtos;
using EntityApi.Entities;

namespace EntityApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Entity, EntityDto>();
            CreateMap<EntityDto, Entity>();
        }
    }
}