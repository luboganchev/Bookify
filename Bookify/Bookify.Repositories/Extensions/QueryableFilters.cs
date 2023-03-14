using Bookify.Entities;

namespace Bookify.Repositories.Extensions
{
    public static class QueryableFilters
    {
        public static IQueryable<T> FilterDeleted<T>(this IQueryable<T> items) where T : ISoftDelete
        {
            var result = items.Where(i => !i.IsDeleted);

            return result;
        }
    }
}
