using System;
using System.Collections.Generic;
using System.Text;

namespace MicroFx.Mapper
{
    public static class IMapperExtensions
    {
        public static TDest Map<TSource, TDest>(this TSource source)
        {
            var mapper = new Mapper<TSource, TDest>();
            return mapper.Map(source);
        }
    }
}
