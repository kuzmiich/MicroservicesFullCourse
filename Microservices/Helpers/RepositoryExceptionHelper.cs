using System;

namespace Microservices.Helpers
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
