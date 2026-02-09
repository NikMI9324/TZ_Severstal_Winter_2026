using Severstal.Domain.Exeptions;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace Severstal.Infrastructure.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyRangeIfExists<T, TValue>(
            this IQueryable<T> query,
            TValue? min,
            TValue? max,
            Expression<Func<T, TValue?>> propertySelector)
            where TValue : struct, IComparable<TValue>
        {
            if (!min.HasValue && !max.HasValue)
                return query;
            
            if (!min.HasValue || !max.HasValue)
                throw new InvalidRangeValuesException(
                    "Для фильтрации по диапазону должны быть указаны оба значения (min и max)");

            Expression? filterExpression = null;

            var property = propertySelector.Body;
            var param = propertySelector.Parameters[0];

            var isNotNull = Expression.NotEqual(property, Expression.Constant(null, typeof(TValue?)));

            var propValue = Expression.Property(property, "Value");

            var greaterThanOrEqual = Expression.GreaterThanOrEqual(propValue, Expression.Constant(min.Value));
            var lessThanOrEqual = Expression.LessThanOrEqual(propValue, Expression.Constant(max.Value));

            filterExpression = Expression.AndAlso(isNotNull, Expression.AndAlso(greaterThanOrEqual, lessThanOrEqual));


            var lambda = Expression.Lambda<Func<T, bool>>(filterExpression,param);

            return query.Where(lambda);

        }
    }
}
