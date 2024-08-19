using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.DTOs.Category;
using Portfolio.DTOs.Technology;
using Portfolio.Entities;

namespace Portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : CustomControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(ApplicationDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetCategories")]
        public async Task<ActionResult<List<Category>>> GetAll()
        {
            return await Get<Category, Category>();
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            return await Get<Category, Category>(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryCreationDTO categoryCreationDTO)
        {
            var categoryExist = await _context.Categories
                .AnyAsync(c => c.Name.ToLower() == categoryCreationDTO.Name.ToLower());

            if (categoryExist)
                return Conflict();

            return await Post<CategoryCreationDTO, Category, Category>
                (categoryCreationDTO, "GetCategory");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryCreationDTO categoryCreationDTO)
        {
            return await Put<CategoryCreationDTO, Category>(id, categoryCreationDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Category>(id);
        }
    }
}
