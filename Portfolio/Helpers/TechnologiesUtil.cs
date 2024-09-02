using Microsoft.EntityFrameworkCore;
using Portfolio.Entities.Interfaces;

/*
 * RemoveAssociations : Elimina todas las asociaciones de una tabla con
 *  las tecnologias vinculadas
 */

namespace Portfolio.Helpers
{
    public class TechnologiesUtil
    {
        public static async Task<Boolean> RemoveAssociations<TEntity, TEntityAssociation>(ApplicationDbContext context, int id, TEntity entity)
            where TEntity : class, IWithTechnologies
            where TEntityAssociation : class, IWithTechnology
        {
            try
            {
                var technologiesIds = entity.TechnologyIds;
                var technologiesWithEntity = await context.Set<TEntityAssociation>()
                    .Where(x =>
                        technologiesIds.Contains(x.TechnologyId) && x.AssociationId == id
                    )
                    //.Select(x => new { x.AssociationId, x.TechnologyId })
                    .ToListAsync();

                if (technologiesWithEntity.Any())
                {
                    context.Set<TEntityAssociation>().RemoveRange(technologiesWithEntity);
                    await context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
