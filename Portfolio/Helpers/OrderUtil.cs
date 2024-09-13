using Microsoft.EntityFrameworkCore;
using Portfolio.Entities.Interfaces;

namespace Portfolio.Helpers
{
    public class OrderUtil
    {
        public async static Task OrderConflict<TEntity, TEntityCreation>
            (ApplicationDbContext context, TEntityCreation entityCreation)
            where TEntity : class, IOrder
            where TEntityCreation : class, IOrder
        {
            {
                var orderConflict = await context
                    .Set<TEntity>()
                    .AnyAsync(t => t.Order == entityCreation.Order);

                if (orderConflict)
                {
                    await SetOrder<TEntity, TEntityCreation>(context, entityCreation);
                }
            }
        }

        private async static Task SetOrder<TEntity, TEntityCreation>
            (ApplicationDbContext context, TEntityCreation entityCreation)
             where TEntity : class, IOrder
             where TEntityCreation : class, IOrder
        {
            var highestOrderSkill = await context
                        .Set<TEntity>()
                        .OrderByDescending(x => x.Order)
                        .FirstOrDefaultAsync();

            entityCreation.Order = highestOrderSkill.Order + 1;
        }
    }
}
