using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.DTOs.Skills.SoftSkill;
using Portfolio.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        [HttpGet("{id:int}",Name = "GetSoftSkill")]
        public async Task<ActionResult<SoftSkill>> Get()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SoftSkillCreationDTO softSkillCreation)
        {
            var ids = softSkillCreation.TechnologiesIds;
            bool technologiesExist = await _context.Technologies
                .Where(t => ids.Contains(t.Id))
                .CountAsync() == ids.Count;

            if (!technologiesExist)
                return NotFound();

            return await Post<SoftSkillCreationDTO, SoftSkill, SoftSkill>
                (softSkillCreation, "GetSoftSkill");
        }
    }
}
