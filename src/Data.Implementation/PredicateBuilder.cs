using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SourceName.Data.Implementation
{
    public static class PredicateBuilder
    {
        public static Expression<Func<TEntity, bool>> True<TEntity>() => param => true;
        public static Expression<Func<TEntity, bool>> False<TEntity>() => param => false;

        public static Expression<Func<TEntity, bool>> Create<TEntity>(
            Expression<Func<TEntity, bool>> predicate) => predicate;

        public static Expression<Func<TEntity, bool>> And<TEntity>(
            this Expression<Func<TEntity, bool>> first,
            Expression<Func<TEntity, bool>> second) => first.Compose(second, Expression.AndAlso);

        public static Expression<Func<TEntity, bool>> Or<TEntity>(
            this Expression<Func<TEntity, bool>> first,
            Expression<Func<TEntity, bool>> second) => first.Compose(second, Expression.OrElse);

        public static Expression<Func<TEntity, bool>> Not<TEntity>(this Expression<Func<TEntity, bool>> expression)
        {
            var negated = Expression.Not(expression.Body);
            return Expression.Lambda<Func<TEntity, bool>>(negated, expression.Parameters);
        }

        static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            var map = first.Parameters
                .Select((f, i) => new { f, s = second.Parameters[i] })
                .ToDictionary(p => p.s, p => p.f);

            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        class ParameterRebinder : ExpressionVisitor
        {
            readonly Dictionary<ParameterExpression, ParameterExpression> map;

            ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }

            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            {
                return new ParameterRebinder(map).Visit(exp);
            }

            protected override Expression VisitParameter(ParameterExpression p)
            {
                ParameterExpression replacement;
                if (map.TryGetValue(p, out replacement))
                {
                    p = replacement;
                }
                return base.VisitParameter(p);
            }
        }

    }
}