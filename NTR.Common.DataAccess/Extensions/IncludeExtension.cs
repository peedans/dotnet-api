using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace NTR.Common.DataAccess.Extensions
{
    public static class IncludeExtension
    {
        public static IQueryable<TEntity> IncludeMany<TEntity, TProperty>(
             [NotNull] this IQueryable<TEntity> source,
             [NotNull] Expression<Func<TEntity, TProperty>> navigationPropertyPath,
             [NotNull] params Expression<Func<TProperty, object>>[] nextProperties
             )
             where TEntity : class
        {
            foreach (var nextProperty in nextProperties)
            {
                source = source.Include(navigationPropertyPath)
                    .ThenInclude(nextProperty);
            }

            return source;
        }

        public static IQueryable<TEntity> IncludeMany<TEntity, TProperty>(
            [NotNull] this IQueryable<TEntity> source,
            [NotNull] Expression<Func<TEntity, IEnumerable<TProperty>>> navigationPropertyPath,
            [NotNull] params Expression<Func<TProperty, object>>[] nextProperties)
            where TEntity : class
        {
            foreach (var nextProperty in nextProperties)
            {
                source = source.Include(navigationPropertyPath)
                    .ThenInclude(nextProperty);
            }

            return source;
        }
    }
}
