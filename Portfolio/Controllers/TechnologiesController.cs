using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.DTOs.Technology;
using Portfolio.Entities;
using Portfolio.Services.Storage;

namespace Portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologiesController : CustomControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorage _fileStorage;
        private readonly string folderContainerFiles = "Technologies";

        public TechnologiesController(ApplicationDbContext context, IMapper mapper, IFileStorage fileStorage)
            : base(context, mapper, fileStorage)
        {
            _context = context;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }

        [HttpGet(Name = "GetTechnologies")]
        public async Task<ActionResult<List<TechnologyDTO>>> GetAll()
        {
            return await Get<Technology, TechnologyDTO>();
        }

        [HttpGet("{id:int}", Name = "GetTechnology")]
        public async Task<ActionResult<TechnologyDTO>> Get(int id)
        {
            return await Get<Technology, TechnologyDTO>(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] TechnologyCreationDTO technologyCreationDTO)
        {
            var categoryExist = await _context.Categories
                .AnyAsync(c => c.Id == technologyCreationDTO.CategoryId);

            if (!categoryExist)
                return BadRequest();

            var technologyExist = await _context.Technologies
               .AnyAsync(t => t.Name.ToLower() == technologyCreationDTO.Name.ToLower());

            if (technologyExist)
                return Conflict();

            return await PostWithImage<TechnologyCreationDTO, Technology, TechnologyDTO>
                (technologyCreationDTO, "GetTechnology", folderContainerFiles);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] TechnologyCreationDTO technologyCreationDTO)
        {
            var categoryExist = await _context.Categories
                .AnyAsync(c => c.Id == technologyCreationDTO.CategoryId);

            if (!categoryExist)
                return BadRequest();

            return await PutWithImage<TechnologyCreationDTO, Technology>
                (id, technologyCreationDTO, folderContainerFiles);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Technology>(id);
        }
    }
}
