using System;

namespace Microservices.Helpers
{
    public static class RepositoryExceptionHelper
    {
        public static void IsIdValid(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException($"Id ({id}) - is not valid! Please enter correct id!");
            }
        }

        public static void IsEntityExists<TEntity>(TEntity entity, string entityName)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity),$"Entity didn't exist in storage, entityName - {entityName}");
            }
        }
    }
}
