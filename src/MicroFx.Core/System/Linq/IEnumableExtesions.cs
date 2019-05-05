using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace System.Linq
{
    public static class IEnumableExtesions
    {
        public static string ToString(this IEnumerable<string> source,string separator,Func<string,string> formaterFun=null)
        {
            formaterFun = formaterFun ?? ((item) => item);
            return string.Join(separator, source.Select(formaterFun));
        }
    }
}
