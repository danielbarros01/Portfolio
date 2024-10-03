using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.DTOs.Education;
using Portfolio.DTOs.Project;
using Portfolio.DTOs.Skills.TechnologySkill;
using Portfolio.Entities;
using Portfolio.Helpers;
using Portfolio.Services.Storage;
using System.ComponentModel;
using System.IO;
using System.Security.Claims;

namespace Portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationsController : CustomControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorage _fileStorage;
        private readonly string _folderContainerFiles = "Educations";
        private readonly string readmesFolderPath = "Educations/readmes";

        public EducationsController
            (ApplicationDbContext context, IMapper mapper, IFileStorage fileStorage)
            : base(context, mapper, fileStorage)
        {
            _context = context;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }

        [HttpGet("{id:int}", Name = "GetEducation")]
        public async Task<ActionResult<EducationDTO>> Get(int id)
        {
            return await Get<Education, EducationDTO>(
                    id,
                    query => query
                        .Include(p => p.EducationsTechnologies)
                        .ThenInclude(pt => pt.Technology)
                );
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EducationDTO>>> Get()
        {
            return await Get<Education, EducationDTO>
                (
                    query => query
                        .Include(e => e.EducationsTechnologies)
                        .ThenInclude(et => et.Technology)
                );
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] EducationCreationDTO educationCreation)
        {
            bool allTechnologiesExist = await TechnologiesUtil.ValidateTechnologiesExistence
                (_context, educationCreation);

            if (!allTechnologiesExist)
                return NotFound();

            await OrderUtil.OrderConflict<Education, EducationCreationDTO>(_context, educationCreation);

            return await PostWithImageAndReadme<EducationCreationDTO, Education, EducationDTO>
                (educationCreation, "GetEducation", _folderContainerFiles, readmesFolderPath);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] EducationCreationDTO educationCreation)
        {
            bool allTechnologiesExist = await TechnologiesUtil.ValidateTechnologiesExistence
               (_context, educationCreation);

            if (!allTechnologiesExist)
                return NotFound();

            var removeAssociations = await TechnologiesUtil.RemoveAssociations<EducationCreationDTO, EducationsTechnologies>
                (_context, id, educationCreation);

            if (!removeAssociations)
                return NotFound();

            return await PutWithImageAndReadme<EducationCreationDTO, Education>(id, educationCreation, _folderContainerFiles, readmesFolderPath);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Education>(id);
        }

    }
}
