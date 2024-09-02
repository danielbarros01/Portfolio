using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.DTOs.Project;
using Portfolio.Entities.Interfaces;
using Portfolio.Services;

namespace Portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsTechnologiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectsTechnologiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        
    }
}
