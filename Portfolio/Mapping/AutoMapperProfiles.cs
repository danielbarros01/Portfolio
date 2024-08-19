using AutoMapper;
using Portfolio.DTOs.Category;
using Portfolio.DTOs.Technology;
using Portfolio.Entities;

namespace Portfolio.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Technology, TechnologyDTO>().ReverseMap();
            CreateMap<TechnologyCreationDTO, Technology>()
                .ForMember(x => x.ImageUrl, opt => opt.Ignore());

            CreateMap<CategoryCreationDTO, Category>();
        }
    }
}
