using System;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace MicroFx.Cryptography.Test
{
    public class FNV1Test
    {
        [Fact(DisplayName ="Create FNV1")]
        public void CreateTest()
        {
            var fnv32 = FNV1.Create();
            Assert.NotNull(fnv32);            
            Assert.IsAssignableFrom<FNV1>(fnv32);

            var fnv64=FNV1.Create(FNVBits.Bits64);
            Assert.NotNull(fnv64);
            Assert.IsAssignableFrom<FNV1>(fnv64);

            Assert.NotNull(FNV1.Create(FNVBits.Bits32));
            Assert.IsAssignableFrom<FNV1>(FNV1.Create(FNVBits.Bits32));

            Assert.Throws<NotSupportedException>(() => {
                FNV1.Create(string.Empty);
            });


        }

        [Fact(DisplayName ="Computer FNV1")]
        public void ComputerTest()
        {
            var bytes= Encoding.UTF8.GetBytes("FNV1-Test");

            var fnv32 = FNV1.Create();
            var v1 = BitConverter.ToUInt32(fnv32.ComputeHash(bytes));
            var v2 = BitConverter.ToUInt32(fnv32.ComputeHash(bytes));
            Assert.NotEqual(v1, v2);
            var v1New = BitConverter.ToUInt32(FNV1.Create().ComputeHash(bytes));
            Assert.Equal(v1, v1New);

            var fnv64 = FNV1.Create(FNVBits.Bits64);
            var v3 = BitConverter.ToUInt64(fnv64.ComputeHash(bytes));
            var v4 = BitConverter.ToUInt64(fnv64.ComputeHash(bytes));
            Assert.NotEqual(v3, v4);
            var v3New = BitConverter.ToUInt64(FNV1.Create(FNVBits.Bits64).ComputeHash(bytes));
            Assert.Equal(v3, v3New);
        }
    }
}
