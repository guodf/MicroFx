using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace MicroFx.Expressions
{
    public interface IFuncItem<T>
    {
        T Execute(object[] objs);
    }
    public class FuncItem<TClassType> : IFuncItem<TClassType>
    {
        Func<object[], TClassType> _func;
        public FuncItem(Func<object[], TClassType> func)
        {
            _func = func;
        }

        public TClassType Execute(object[] objs)
        {
            return _func(objs);
        }
    }

    public static class ExpressionHelper<TClassType>
    {
        public static ConcurrentDictionary<Type, Func<object[], TClassType>> _catch = new ConcurrentDictionary<Type, Func<object[], TClassType>>();

        public static TClassType New(params object[] objs)
        {
            var type = typeof(TClassType);
            return _catch.GetOrAdd(type, _ =>
            {
                var argsType = objs.Select(item => item.GetType()).ToArray();
                var constructor = type.GetConstructor(argsType);
                var paramExpression = Expression.Parameter(objs.GetType());
                var paramsExression = new List<UnaryExpression>();
                var @params = objs.Object2Expression();
                for (var index= 0;index < objs.Length;index++)
                {
                    var expression = Expression.ArrayIndex(paramExpression, Expression.Constant(index));
                    paramsExression.Add(Expression.Convert(expression, @params[index].Type));
                }
                var newExpression = Expression.New(constructor, paramsExression);
                var lambda = Expression.Lambda<Func<object[], TClassType>>(newExpression,paramExpression);
                return lambda.Compile();

            })(objs);
        }

        public static TClassType New<TParameterType>(TParameterType arg)
        {
            return New(new object[] { arg });

        }

        public static TClassType New<TParameter1Type, TParameter2Type>(TParameter1Type arg1, TParameter2Type arg2)
        {
            return New(new object[] { arg1, arg2 });
        }
    }
}
