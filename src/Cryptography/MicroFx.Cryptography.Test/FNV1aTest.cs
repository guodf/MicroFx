using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace MicroFx.Cryptography.Test
{
    public class FNV1aTest
    {
        [Fact(DisplayName ="Create FNV1a")]
        public void CreateTest()
        {
            Assert.NotNull(FNV1a.Create());
            Assert.IsAssignableFrom<FNV1a>(FNV1a.Create());
            Assert.NotNull(FNV1a.Create(FNVBits.Bits32));
            Assert.IsAssignableFrom<FNV1a>(FNV1a.Create(FNVBits.Bits32));
            Assert.NotNull(FNV1a.Create(FNVBits.Bits64));
            Assert.IsAssignableFrom<FNV1a>(FNV1a.Create(FNVBits.Bits64));
            Assert.Throws<NotSupportedException>(() =>
            {
                FNV1a.Create(null);
            });

        }
        [Fact(DisplayName ="Computer FNV1a")]
        public void ComputerTest()
        {
            var bytes = Encoding.UTF8.GetBytes("FNV1a-Test");

            var v1 = BitConverter.ToUInt32(FNV1a.Create().ComputeHash(bytes));
            var v2 = BitConverter.ToUInt32(FNV1a.Create().ComputeHash(bytes));
            Assert.Equal(v1, v2);

            var v3 = BitConverter.ToUInt64(FNV1a.Create(FNVBits.Bits64).ComputeHash(bytes));
            var v4 = BitConverter.ToUInt64(FNV1a.Create(FNVBits.Bits64).ComputeHash(bytes));
            Assert.Equal(v3, v4);
        }
    }
}
