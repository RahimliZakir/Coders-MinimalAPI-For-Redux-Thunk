using AutoMapper;
using Coders.WebAPI.Models.Entities;

namespace Coders.WebAPI.AppCode.Mappers.Profiles
{
    public class CoderProfile : Profile
    {
        public CoderProfile()
        {
            CreateMap<Coder, Coder>();
        }
    }
}
