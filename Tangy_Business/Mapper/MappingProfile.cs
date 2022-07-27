using AutoMapper;
using Tangy.Data.Models;
using Tangy.Models;

namespace Tangy.Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
