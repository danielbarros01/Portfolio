using AutoMapper;
using Portfolio.DTOs.Category;
using Portfolio.DTOs.Project;
using Portfolio.DTOs.Skills.SoftSkill;
using Portfolio.DTOs.Skills.TechnologySkill;
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

            CreateMap<TechnicalSkillCreationDTO, TechnicalSkill>().ReverseMap();
            CreateMap<TechnicalSkill, TechnicalSkillDTO>().ReverseMap();

            CreateMap<SoftSkillCreationDTO, SoftSkill>().ReverseMap();

            CreateMap<ProjectCreationDTO, Project>()
                .ForMember(x => x.ImageUrl, options => options.Ignore())
                .ForMember(
                    x => x.ProjectsTechnologies,
                    options => options.MapFrom(MapProjectsTechnologies)
                );

            CreateMap<ProjectDTO, Project>()
                .ReverseMap()
                .ForMember(x =>
                    x.Technologies,
                    options => options.MapFrom(MapTechnologiesProject)
                );
        }

        private List<Technology> MapTechnologiesProject
            (Project project, ProjectDTO projectDTO)
        {
            var result = new List<Technology>();
            if(project.ProjectsTechnologies == null) { return result; }

            foreach(var pTechnology in project.ProjectsTechnologies)
            {
                result.Add(pTechnology.Technology);
            }

            return result;
        }

        private List<ProjectsTechnologies> MapProjectsTechnologies
            (ProjectCreationDTO projectCreation, Project project)
        {
            var result = new List<ProjectsTechnologies>();
            if (projectCreation.TechnologyIds == null) { return result; }

            foreach (var id in projectCreation.TechnologyIds)
            {
                result.Add(new ProjectsTechnologies() { TechnologyId = id });
            }

            return result;
        }
    }
}
