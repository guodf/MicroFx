using System;
using System.Linq.Expressions;
using Xunit;

namespace MicroFx.Expressions.Test
{
    public class ExpressionExtensionsTest
    {
        class A
        {
            public A()
            { }
            string _a;
            int _b;
            public A(string a)
            {
                _a = a;
            }

            public A(string a, int b)
            {
                _a = a;
                _b = b;
            }
        }

        [Fact]
        public void NewTest()
        {
            Expression<Func<object,A>> aFun = (a) => new A((string)a);
            

           // ExpressionHelper<A>.New();

            var aa=ExpressionHelper<A>.New("aa");

            aa=ExpressionHelper<A>.New("aa", 1);
        }
    }
}
