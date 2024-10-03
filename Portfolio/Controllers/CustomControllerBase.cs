using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.DTOs.Others;
using Portfolio.DTOs.Technology;
using Portfolio.Entities.Interfaces;
using Portfolio.Extensions;
using Portfolio.Helpers;
using Portfolio.Services.Storage;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Portfolio.Controllers
{
    public class CustomControllerBase : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorage _fileStorage;

        public CustomControllerBase(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public CustomControllerBase(ApplicationDbContext context, IMapper mapper, IFileStorage fileStorage)
        {
            _context = context;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }

        protected async Task<List<TDTO>> Get<TEntity, TDTO>(
            Func<IQueryable<TEntity>, IQueryable<TEntity>> queryFunc = null
            ) where TEntity : class
        {
            var queryable = _context.Set<TEntity>()
                .AsNoTracking();

            if (queryFunc != null)
            {
                queryable = queryFunc(queryable);
            }

            var entities = await queryable.ToListAsync();
            var dtos = _mapper.Map<List<TDTO>>(entities);

            return dtos;
        }

        protected async Task<ActionResult<TDTO>> Get<TEntity, TDTO>(
            int id,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> queryFunc = null
            )
            where TEntity : class, IId
        {
            var queryable = _context.Set<TEntity>()
                .AsNoTracking();

            if (queryFunc != null)
            {
                queryable = queryFunc(queryable);
            }

            var entity = await queryable.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return NotFound();

            return _mapper.Map<TDTO>(entity);
        }

        protected async Task<List<TDTO>> Get<TEntity, TDTO>(
            PaginationDTO paginationDTO,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> queryFunc = null
            )
            where TEntity : class
        {
            var queryable = _context.Set<TEntity>().AsQueryable();

            if (queryFunc != null)
            {
                queryable = queryFunc(queryable);
            }

            await HttpContext.InsertNumberOfPages(queryable, paginationDTO.RecordsPerPage);
            var entities = await queryable.Paginate(paginationDTO).ToListAsync();
            return _mapper.Map<List<TDTO>>(entities);
        }


        protected async Task<ActionResult> Post<TCreation, TEntity, TRead>
            (TCreation creationDTO, string routeName) where TEntity : class, IId
        {
            var entity = _mapper.Map<TEntity>(creationDTO);

            _context.Add(entity);
            await _context.SaveChangesAsync();

            var readEntity = _mapper.Map<TRead>(entity);

            return new CreatedAtRouteResult(routeName, new { id = entity.Id }, readEntity);
        }


        protected async Task<ActionResult> PostWithImage<TCreation, TEntity, TRead>
            (TCreation creationDTO, string routeName, string imageContainer)
            where TEntity : class, IId, IHasImageUrl
            where TCreation : IHasImage
        {
            var entity = _mapper.Map<TEntity>(creationDTO);

            var image = creationDTO.Image;
            if (image != null)
            {
                entity.ImageUrl = await FilesUtil.GetUrlFile
                    (image, _fileStorage, imageContainer, ActionForFileType.Save);
            }

            _context.Add(entity);
            await _context.SaveChangesAsync();

            var readEntity = _mapper.Map<TRead>(entity);

            return new CreatedAtRouteResult(routeName, new { id = entity.Id }, readEntity);
        }

        protected async Task<ActionResult> Put<TCreation, TEntity>
            (int id, TCreation creationDTO) where TEntity : class, IId
        {
            var entity = await _context.Set<TEntity>()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return NotFound();

            _mapper.Map(creationDTO, entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        protected async Task<ActionResult> PutWithImage<TCreation, TEntity>
           (int id, TCreation creationDTO, string imageContainer)
            where TEntity : class, IId, IHasImageUrl
            where TCreation : IHasImage
        {
            var entity = await _context.Set<TEntity>()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return NotFound();

            var image = creationDTO.Image;
            if (image != null)
            {
                entity.ImageUrl = await FilesUtil.GetUrlFile
                    (image, _fileStorage, imageContainer, ActionForFileType.Edit);
            }

            _mapper.Map(creationDTO, entity);

            await _context.SaveChangesAsync();

            return NoContent();
        }


        protected async Task<ActionResult> Delete<TEntity>(int id) where TEntity : class, IId
        {
            var entity = await _context.Set<TEntity>()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (entity == null)
                return NotFound();

            _context.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        protected async Task<ActionResult> PostWithImageAndReadme<TCreation, TEntity, TRead>
            (TCreation creationDTO, string routeName, string imageContainer, string readmeContainer)
            where TEntity : class, IId, IHasImageUrl, IReadmeUrl
            where TCreation : IHasImage, IReadme
        {
            var entity = _mapper.Map<TEntity>(creationDTO);

            var image = creationDTO.Image;
            if (image != null)
            {
                entity.ImageUrl = await FilesUtil.GetUrlFile
                    (image, _fileStorage, imageContainer, ActionForFileType.Save);
            }

            var readme = creationDTO.Readme;
            if (readme != null)
            {
                entity.ReadmeUrl = await FilesUtil.GetUrlFile
                    (readme, _fileStorage, readmeContainer, ActionForFileType.Save);
            }

            var readmeES = creationDTO.ReadmeES;
            if (readmeES != null)
            {
                entity.ReadmeUrlES = await FilesUtil.GetUrlFile
                    (readmeES, _fileStorage, readmeContainer, ActionForFileType.Save);
            }

            _context.Add(entity);
            await _context.SaveChangesAsync();

            var readEntity = _mapper.Map<TRead>(entity);

            return new CreatedAtRouteResult(routeName, new { id = entity.Id }, readEntity);
        }

        protected async Task<ActionResult> PutWithImageAndReadme<TCreation, TEntity>
           (int id, TCreation creationDTO, string imageContainer, string readmeContainer)
            where TEntity : class, IId, IHasImageUrl, IReadmeUrl
            where TCreation : IHasImage, IReadme
        {
            var entity = await _context.Set<TEntity>()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return NotFound();

            var image = creationDTO.Image;
            if (image != null)
            {
                entity.ImageUrl = await FilesUtil.GetUrlFile
                    (image, _fileStorage, imageContainer, ActionForFileType.Edit);
            }

            var readme = creationDTO.Readme;
            if (readme != null) {
                entity.ReadmeUrl = await FilesUtil.GetUrlFile
                    (readme, _fileStorage, readmeContainer, ActionForFileType.Edit);
            }

            var readmeES = creationDTO.ReadmeES;
            if (readmeES != null)
            {
                entity.ReadmeUrlES = await FilesUtil.GetUrlFile
                    (readmeES, _fileStorage, readmeContainer, ActionForFileType.Edit);
            }

            _mapper.Map(creationDTO, entity);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
