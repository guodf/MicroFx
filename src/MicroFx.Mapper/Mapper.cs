using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Generic;

namespace MicroFx.Mapper
{
    public class Mapper
    {
        private static ConcurrentDictionary<(Type, Type), Delegate> _mapDict = new ConcurrentDictionary<(Type, Type), Delegate>();

        public static Mapper<TSource,TDest> CreateMap<TSource, TDest>()
        {
            return new Mapper<TSource,TDest>();
        }

        internal static Delegate GetOrAdd<TSource,TDest>(Delegate @delegate)
        {
            return _mapDict.GetOrAdd((typeof(TSource),typeof(TDest)), @delegate);   
        }
    }

    public class Mapper<TSource, TDest>
    {
        public TDest Map(TSource source)
        {
            return ((Func<TSource,TDest>)Mapper.GetOrAdd<TSource, TDest>(MakeMapper()))(source);
        }
        private Func<TSource,TDest> MakeMapper()
        {
            var sourceType = typeof(TSource);
            var destType = typeof(TDest);
            /**
             * 1. 获取所有public属性
             * 2. 获取可读属性
            */
            var sourceProperties = sourceType.GetProperties(BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance).ToDictionary(item => item.Name);
            /**
             * 1. 获取所有public属性
             * 2. 获取可写属性
             */
            var destProperties = destType.GetProperties(BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance).ToDictionary(item => item.Name);

            var destInstanceExpression = Expression.New(destType);
            var sourceArg = Expression.Parameter(sourceType, "source");
            //查找名称相同且类型相同的成员（区分大小写）
            var samePropertyItems = destProperties.Intersect(sourceProperties, new PropertiesCompare());

            var assignList = new List<MemberAssignment>();
            foreach (var item in samePropertyItems)
            {
                var name = item.Key;
                assignList.Add(Expression.Bind(item.Value, Expression.MakeMemberAccess(sourceArg, sourceProperties[name])));
            }

            var block = Expression.MemberInit(destInstanceExpression, assignList);

            return Expression.Lambda<Func<TSource, TDest>>(block, sourceArg).Compile();
        }
    }

    class PropertiesCompare : IEqualityComparer<KeyValuePair<string,PropertyInfo>>
    {
        public bool Equals(KeyValuePair<string, PropertyInfo> x, KeyValuePair<string, PropertyInfo> y)
        {
            return x.Key == y.Key && x.Value.PropertyType == y.Value.PropertyType;
        }

        public int GetHashCode(KeyValuePair<string, PropertyInfo> obj)
        {
            return  obj.GetHashCode();
        }
    }

}
