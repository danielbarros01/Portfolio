using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.DTOs.Project;
using Portfolio.DTOs.Technology;
using Portfolio.Entities;
using Portfolio.Helpers;
using Portfolio.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : CustomControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorage _fileStorage;
        private readonly string folderContainerFiles = "Projects";

        public ProjectsController(ApplicationDbContext context, IMapper mapper, IFileStorage fileStorage)
            : base(context, mapper, fileStorage)
        {
            _context = context;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }

        [HttpGet(Name = "GetProjects")]
        public async Task<ActionResult<List<ProjectDTO>>> Get()
        {
            return await Get<Project,ProjectDTO>(
                    query => query
                        .Include(p => p.ProjectsTechnologies)
                        .ThenInclude(pt => pt.Technology)
                );
        }

        [HttpGet("{id:int}", Name = "GetProject")]
        public async Task<ActionResult<ProjectDTO>> Get(int id)
        {
            return await Get<Project, ProjectDTO>(
                    id,
                    query => query
                        .Include(p => p.ProjectsTechnologies)
                        .ThenInclude(pt => pt.Technology)
                );
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ProjectCreationDTO projectCreation)
        {
            bool allTechnologiesExist = await _context.Technologies
                .Where(t => projectCreation.TechnologyIds.Contains(t.Id))
                .CountAsync() == projectCreation.TechnologyIds.Count();

            if (!allTechnologiesExist)
                return NotFound();

            return await PostWithImage<ProjectCreationDTO, Project, ProjectDTO>
                (projectCreation, "GetProject", folderContainerFiles);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] ProjectCreationDTO projectCreation)
        {
            bool allTechnologiesExist = await _context.Technologies
                .Where(t => projectCreation.TechnologyIds.Contains(t.Id))
                .CountAsync() == projectCreation.TechnologyIds.Count();

            if (!allTechnologiesExist)
                return NotFound();

            var result = await TechnologiesUtil.RemoveAssociations<ProjectCreationDTO, ProjectsTechnologies>
                (_context, id, projectCreation);

            return await Put<ProjectCreationDTO, Project>(id, projectCreation);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Project>(id);
        }
    }
}
