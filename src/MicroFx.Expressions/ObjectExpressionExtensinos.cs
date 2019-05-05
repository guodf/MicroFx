using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace System
{
    public static class ObjectExpressionExtensinos
    {
        public static ParameterExpression Object2Expression(this object obj,string name=null)
        {
            if (obj == null)
            {
                return null;
            }
            return string.IsNullOrWhiteSpace(name)? Expression.Parameter(obj.GetType()):Expression.Parameter(obj.GetType(),name);
        }

        public static ParameterExpression[] Object2Expression(this object[] objs)
        {
            if (objs == null)
            {
                return null;
            }
            var parameterExpreesions = new List<ParameterExpression>();
            
            foreach (var obj in objs)
            {
                parameterExpreesions.Add(obj.Object2Expression());
            }
            return parameterExpreesions.ToArray();
        }

        public static ParameterExpression Object2Expression<TClassType>(this TClassType classType,string name=null)
        {
            if (classType == null)
            {
                return null;
            }

            return string.IsNullOrWhiteSpace(name)?  Expression.Parameter(typeof(TClassType)):Expression.Parameter(typeof(TClassType),name);
        }
    }
}
