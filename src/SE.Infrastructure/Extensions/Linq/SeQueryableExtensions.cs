using System.Linq.Expressions;
using JetBrains.Annotations;
using SqlSugar;

namespace SE.Infrastructure.Extensions.Linq;

/// <summary>
/// Some useful extension methods for <see cref="SqlSugar.ISugarQueryable{T}"/>.
/// </summary>
public static class SeQueryableExtensions
{
    /// <summary>
    /// Used for paging. Can be used as an alternative to Skip(...).Take(...) chaining.
    /// </summary>
    public static ISugarQueryable<T> PageBy<T>([NotNull] this ISugarQueryable<T> query, int skipCount, int maxResultCount)
    {
        Check.NotNull(query, nameof(query));

        return query.Skip(skipCount).Take(maxResultCount);
    }

    /// <summary>
    /// Filters a <see cref="ISugarQueryable{T}"/> by given predicate if given condition is true.
    /// </summary>
    /// <param name="query">Queryable to apply filtering</param>
    /// <param name="condition">A boolean value</param>
    /// <param name="predicate">Predicate to filter the query</param>
    /// <returns>Filtered or not filtered query based on <paramref name="condition"/></returns>
    public static ISugarQueryable<T> WhereIf<T>([NotNull] this ISugarQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
    {
        Check.NotNull(query, nameof(query));

        return condition
            ? query.Where(predicate)
            : query;
    }

    /// <summary>
    /// Filters a <see cref="IQueryable{T}"/> by given predicate if given condition is true.
    /// </summary>
    /// <param name="query">Queryable to apply filtering</param>
    /// <param name="condition">A boolean value</param>
    /// <param name="predicate">Predicate to filter the query</param>
    /// <returns>Filtered or not filtered query based on <paramref name="condition"/></returns>
    public static TQueryable WhereIf<T, TQueryable>([NotNull] this TQueryable query, bool condition, Expression<Func<T, bool>> predicate)
        where TQueryable : IQueryable<T>
    {
        Check.NotNull(query, nameof(query));

        return condition
            ? (TQueryable)query.Where(predicate)
            : query;
    }
}
