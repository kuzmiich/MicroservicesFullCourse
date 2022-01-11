using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KuzmichInc.Microservices.PlatformService.Helpers
{
    public static class RepositoryExceptionHelper
    {
        public static void IsIdValid(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }
        }

        public static void IsEntityExists<TEntity>(TEntity entity, string entityName)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(entityName);
            }
        }
    }
}
