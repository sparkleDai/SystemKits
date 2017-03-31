using System.Linq.Expressions;

namespace System
{
    /// <summary>
    /// Expression扩展
    /// </summary>
    public static class ExpressionExtension
    {
        #region 公有变量
        public static Expression<Func<T, bool>> True<T>() { return f => true; }

        public static Expression<Func<T, bool>> False<T>() { return f => false; }
        #endregion

        #region And
        /// <summary>
        /// 表达式的And
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp_left"></param>
        /// <param name="exp_right"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> exp_left, Expression<Func<T, bool>> exp_right)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            var left = parameterReplacer.Replace(exp_left.Body);
            var right = parameterReplacer.Replace(exp_right.Body);
            var body = Expression.And(left, right);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }
        #endregion

        #region Or
        /// <summary>
        /// 表达式的Or
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp_left"></param>
        /// <param name="exp_right"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> exp_left, Expression<Func<T, bool>> exp_right)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            var left = parameterReplacer.Replace(exp_left.Body);
            var right = parameterReplacer.Replace(exp_right.Body);
            var body = Expression.Or(left, right);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }
        #endregion

        #region ParameterReplacer
        /// <summary>
        /// 统一ParameterExpression(多条件组合)
        /// </summary>
        internal class ParameterReplacer : ExpressionVisitor
        {
            public ParameterReplacer(ParameterExpression paramExpr)
            {
                this.ParameterExpression = paramExpr;
            }

            public ParameterExpression ParameterExpression { get; private set; }

            public Expression Replace(Expression expr)
            {
                return this.Visit(expr);
            }

            protected override Expression VisitParameter(ParameterExpression p)
            {
                return this.ParameterExpression;
            }
        }
        #endregion
       
    }
}
