﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.DTOs.Others;
using Portfolio.DTOs.Skills.TechnologySkill;
using Portfolio.DTOs.Technology;
using Portfolio.Entities;
using Portfolio.Helpers;

namespace Portfolio.Controllers
{
    [Route("api/skills/technical")]
    [ApiController]
    public class TechnicalSkillsController : CustomControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TechnicalSkillsController(ApplicationDbContext context, IMapper mapper)
            : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetTechnicalSkills")]
        public async Task<ActionResult<List<TechnicalSkillDTO>>> Get([FromQuery] PaginationDTO pagination)
        {
            var technicalSkills = await Get<TechnicalSkill, TechnicalSkillDTO>(
                pagination,
                query => query
                .Include(t => t.Technology)
                .OrderBy(t => t.Order)
                );

            return technicalSkills;
        }

        [HttpGet("{id:int}", Name = "GetTechnicalSkill")]
        public async Task<ActionResult<TechnicalSkillDTO>> Get(int id)
        {
            return await Get<TechnicalSkill, TechnicalSkillDTO>(
                id,
                query => query.Include(t => t.Technology)
                );
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TechnicalSkillCreationDTO skillCreationDTO)
        {
            var technologyExists = await _context.Technologies
                .AnyAsync(t => t.Id == skillCreationDTO.TechnologyId);

            if (!technologyExists)
                return BadRequest();

            var technologyAssociated = await _context
                .TechnicalSkills
                .AnyAsync(ts => ts.TechnologyId == skillCreationDTO.TechnologyId);

            if (technologyAssociated)
                return Conflict();

            await OrderUtil.OrderConflict<TechnicalSkill, TechnicalSkillCreationDTO>(_context, skillCreationDTO);

            return await Post<TechnicalSkillCreationDTO, TechnicalSkill, TechnicalSkillDTO>
                (skillCreationDTO, "GetTechnicalSkill");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<TechnicalSkill>> Put(int id, [FromBody] TechnicalSkillCreationDTO skillCreationDTO)
        {
            var technologyExist = await _context.Technologies
                .AnyAsync(t => t.Id == skillCreationDTO.TechnologyId);

            if (!technologyExist)
                return BadRequest();

            return await Put<TechnicalSkillCreationDTO, TechnicalSkill>(id, skillCreationDTO);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<TechnicalSkill>(id);
        }
    }
}
