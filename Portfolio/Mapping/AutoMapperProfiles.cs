using AutoMapper;
using Portfolio.DTOs.Category;
using Portfolio.DTOs.Education;
using Portfolio.DTOs.Project;
using Portfolio.DTOs.Skills.SoftSkill;
using Portfolio.DTOs.Skills.TechnologySkill;
using Portfolio.DTOs.Technology;
using Portfolio.Entities;
using Portfolio.Entities.Interfaces;

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

            CreateMap<TechnicalSkillCreationDTO, TechnicalSkill>().ReverseMap();
            CreateMap<TechnicalSkill, TechnicalSkillDTO>().ReverseMap();

            CreateMap<ProjectCreationDTO, Project>()
                .ForMember(x => x.ImageUrl, options => options.Ignore())
                .ForMember(
                    x => x.ProjectsTechnologies,
                    options => options.MapFrom(src => MapAssociationTechnologies<ProjectsTechnologies>(src.TechnologyIds))
                );

            CreateMap<ProjectDTO, Project>()
                .ReverseMap()
                .ForMember(
                    x => x.Technologies,
                    opt => opt.MapFrom(src => MapTechnologiesProject(src.ProjectsTechnologies))
                );

            CreateMap<SoftSkillCreationDTO, SoftSkill>()
                .ForMember(
                    x => x.SoftSkillsTechnologies,
                    options => options.MapFrom(src => MapAssociationTechnologies<SoftSkillsTechnologies>(src.TechnologyIds))
                );

            CreateMap<SoftSkillDTO, SoftSkill>()
                .ReverseMap()
                .ForMember(
                    x => x.Technologies,
                    opt => opt.MapFrom(src => MapTechnologiesProject(src.SoftSkillsTechnologies))
                );


            CreateMap<EducationCreationDTO, Education>()
                .ForMember(x => x.ImageUrl, options => options.Ignore())
                .ForMember(
                    x => x.EducationsTechnologies,
                    options => options.MapFrom(src => MapAssociationTechnologies<EducationsTechnologies>(src.TechnologyIds))
                );

            CreateMap<EducationDTO, Education>()
                .ReverseMap()
                .ForMember(x =>
                    x.Technologies,
                    opt => opt.MapFrom(src => MapTechnologiesProject(src.EducationsTechnologies))
                );
        }


        private List<Technology> MapTechnologiesProject<TAssociation>
            (IEnumerable<TAssociation> associations)
            where TAssociation : class, IWithTechnology
        {
            var result = new List<Technology>();

            if (associations == null)
                return result;

            foreach (var association in associations)
            {
                result.Add(association.Technology);
            }

            return result;
        }


        private List<TAssociation> MapAssociationTechnologies<TAssociation>
            (IEnumerable<int> technologyIds)
            where TAssociation : class, new()
        {
            var result = new List<TAssociation>();

            if (technologyIds == null)
                return result;

            foreach (var id in technologyIds)
            {
                var association = new TAssociation();
                typeof(TAssociation).GetProperty("TechnologyId")?.SetValue(association, id);
                result.Add(association);
            }

            return result;
        }
    }
}
