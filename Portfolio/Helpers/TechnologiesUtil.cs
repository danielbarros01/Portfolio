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
        public static async Task<Boolean> ValidateTechnologiesExistence<TCreation>
            (ApplicationDbContext context, TCreation entityCreation)
            where TCreation : class, IWithTechnologiesIds
        {
           return await context.Technologies
                .Where(t => entityCreation.TechnologyIds.Contains(t.Id))
                .CountAsync() == entityCreation.TechnologyIds.Count();
        }

        /*
         ACTUALIZAR CODIGO
        ELIMINA LOS NUEVOS QUE VIENEN
        NO LOS QUE YA ESTAN
         */

        public static async Task<Boolean> RemoveAssociations<TEntity, TEntityAssociation>(ApplicationDbContext context, int id, TEntity entity)
            where TEntity : class, IWithTechnologiesIds
            where TEntityAssociation : class, IWithTechnologyId
        {
            try
            {
                var technologiesIds = entity.TechnologyIds;
                var technologiesWithEntity = await context.Set<TEntityAssociation>()
                    .Where(x => x.AssociationId == id
                    )
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
