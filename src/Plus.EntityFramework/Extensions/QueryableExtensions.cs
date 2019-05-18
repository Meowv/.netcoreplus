using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Plus.EntityFramework.Extensions
{
    /// <summary>
    /// QueryableExtensions
    /// </summary>
    public static class QueryableExtensions
    {
        public static IQueryable<T> IncludeIf<T>(this IQueryable<T> source, bool condition, string path) where T : class
        {
            return condition ? EntityFrameworkQueryableExtensions.Include(source, path) : source;
        }

        public static IQueryable<T> IncludeIf<T, TProperty>(this IQueryable<T> source, bool condition, Expression<Func<T, TProperty>> path) where T : class
        {
            object result;
            if (!condition)
            {
                result = source;
            }
            else
            {
                IQueryable<T> queryable = source.Include(path);
                result = queryable;
            }
            return (IQueryable<T>)result;
        }

        public static IQueryable<T> IncludeIf<T>(this IQueryable<T> source, bool condition, Func<IQueryable<T>, IIncludableQueryable<T, object>> include) where T : class
        {
            object result;
            if (!condition)
            {
                result = source;
            }
            else
            {
                IQueryable<T> queryable = include(source);
                result = queryable;
            }
            return (IQueryable<T>)result;
        }
    }
}