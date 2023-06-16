using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Web.Crud
{
    public static class QueryBuilder
    {
        public const int MaxResults = 10000;

        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty, bool isDesc, bool thenById = true)
        {
            if (string.IsNullOrWhiteSpace(orderByProperty))
                return source;

            source = source.OrderBy("Order", orderByProperty, isDesc);
            if (thenById) source = source.OrderBy("Then", "Id", true);
            return source;
        }

        public static IQueryable<TEntity> TakePage<TEntity>(this IQueryable<TEntity> source, int pageIndex, int pageSize)
        {
            if (pageSize > MaxResults)
                throw new NotSupportedException($"Page size more than {MaxResults} not supported");
            return source.Skip(pageIndex * pageSize).Take(pageSize);
        }

        public static IQueryable<TEntity> ApplyPagingAndSorting<TEntity>(this IQueryable<TEntity> source, SearchQuery query, Dictionary<string, string> sortRemap = null)
            where TEntity : class
        {
            if (query == null)
                return source;

            var sortField = query.SortField != null && sortRemap?.ContainsKey(query.SortField) == true
                ? sortRemap[query.SortField] : query.SortField;
            return source.OrderBy(sortField, query.IsDesc).TakePage(query.PageIndex, query.PageSize);
        }

        public static string FirstLetterUp(this string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }

        private static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source,
            string orderClause, string orderByProperty, bool isDesc)
        {
            if (string.IsNullOrWhiteSpace(orderByProperty))
                return source;

            var (arguments, orderByExpression) = orderByProperty.IndexOf('.') < 0
                ? OrderByProperty(orderByProperty, typeof(TEntity))
                : OrderByDependantProperty(orderByProperty, typeof(TEntity));

            var command = orderClause + (isDesc ? "ByDescending" : "By");
            var resultExpression = Expression.Call(typeof(Queryable),
                command, arguments, source.Expression, Expression.Quote(orderByExpression));

            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }

        private static (Type[], LambdaExpression) OrderByProperty(string orderByProperty, Type type)
        {
            var property = type.GetProperty(orderByProperty.FirstLetterUp());
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var arguments = new Type[] { type, property.PropertyType };

            return (arguments, orderByExpression);
        }

        private static (Type[], LambdaExpression) OrderByDependantProperty(string orderByProperty, Type type)
        {
            var orderByProps = orderByProperty.Split(".");
            var property = type.GetProperty(orderByProps[0].FirstLetterUp());

            var subPropType = property.PropertyType;
            for (int i = 1; i < orderByProps.Length; i++)
            {
                subPropType = subPropType.GetProperty(orderByProps[i].FirstLetterUp()).PropertyType;
            }

            var orderByExpression = CreateExpression(type, orderByProperty);
            var arguments = new Type[] { type, subPropType };

            return (arguments, orderByExpression);
        }

        private static LambdaExpression CreateExpression(Type type, string propertyName)
        {
            var param = Expression.Parameter(type, "x");
            Expression body = param;
            foreach (var member in propertyName.Split('.'))
            {
                body = Expression.PropertyOrField(body, member.FirstLetterUp());
            }
            return Expression.Lambda(body, param);
        }
    }
}
