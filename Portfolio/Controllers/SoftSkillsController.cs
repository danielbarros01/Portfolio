using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.DTOs.Others;
using Portfolio.DTOs.Project;
using Portfolio.DTOs.Skills.SoftSkill;
using Portfolio.DTOs.Skills.TechnologySkill;
using Portfolio.Entities;
using Portfolio.Helpers;

namespace Portfolio.Controllers
{
    [Route("api/skills/soft")]
    [ApiController]
    public class SoftSkillsController : CustomControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SoftSkillsController(ApplicationDbContext context, IMapper mapper)
            : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetSoftSkills")]
        public async Task<ActionResult<List<SoftSkillDTO>>> Get([FromQuery] PaginationDTO pagination)
        {
            var softSkillsDTO = await Get<SoftSkill, SoftSkillDTO>(
                pagination,
                query => query
                        .Include(p => p.SoftSkillsTechnologies)
                        .ThenInclude(pt => pt.Technology));

            return softSkillsDTO;
        }

        [HttpGet("{id:int}", Name = "GetSoftSkill")]
        public async Task<ActionResult<SoftSkillDTO>> Get(int id)
        {
            return await Get<SoftSkill, SoftSkillDTO>(
                    id,
                    query => query
                        .Include(p => p.SoftSkillsTechnologies)
                        .ThenInclude(pt => pt.Technology)
                );
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SoftSkillCreationDTO softSkillCreation)
        {
            var ids = softSkillCreation.TechnologyIds;
            bool technologiesExist = await _context.Technologies
                .Where(t => ids.Contains(t.Id))
                .CountAsync() == ids.Count;

            if (!technologiesExist)
                return NotFound();

            return await Post<SoftSkillCreationDTO, SoftSkill, SoftSkillDTO>
                (softSkillCreation, "GetSoftSkill");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] SoftSkillCreationDTO softSkillCreation)
        {
            var ids = softSkillCreation.TechnologyIds;
            bool technologiesExist = await _context.Technologies
                .Where(t => ids.Contains(t.Id))
                .CountAsync() == ids.Count;

            if (!technologiesExist)
                return NotFound();

            var removeAssociations = await TechnologiesUtil.RemoveAssociations<SoftSkillCreationDTO, SoftSkillsTechnologies>
                (_context, id, softSkillCreation);

            if (!removeAssociations)
                return NotFound();


            return await Put<SoftSkillCreationDTO, SoftSkill>(id, softSkillCreation);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<SoftSkill>(id);
        }
    }
}
