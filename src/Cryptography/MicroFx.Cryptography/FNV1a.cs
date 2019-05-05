using System;
using System.Security.Cryptography;

namespace System.Security.Cryptography
{
    public abstract class FNV1a : HashAlgorithm
    {
        public static new FNV1a Create()
        {
            return Create(FNVBits.Bits32);
        }

        public static FNV1a Create(FNVBits fNVBits)
        {
            FNV1a fnv1a = null;
            switch (fNVBits)
            {
                case FNVBits.Bits32:
                    fnv1a = new Implementation32();
                    break;
                case FNVBits.Bits64:
                    fnv1a = new Implementation64();
                    break;
            }
            return fnv1a;
        }
        public static new FNV1a Create(string hashName)
        {
            throw new NotSupportedException();
        }

        private sealed class Implementation32 : FNV1a
        {
            private const uint OFFSETBASIS = 2166136261;

            private const uint PRIME = 16777619;

            private uint _hash;

            public override void Initialize()
            {
                _hash = OFFSETBASIS;
            }

            protected override void HashCore(byte[] array, int ibStart, int cbSize)
            {
                int end = ibStart + cbSize;
                for (var index = ibStart; index < end; index++)
                {
                    _hash ^= array[index];
                    _hash *= PRIME;
                }
            }

            protected override byte[] HashFinal()
            {
                return BitConverter.GetBytes(_hash);
            }
        }

        private sealed class Implementation64 : FNV1a
        {
            private ulong OFFSETBASIS = 14695981039346656037;

            private ulong PRIME = 1099511628211;

            private ulong _hash;

            public override void Initialize()
            {
                _hash = OFFSETBASIS;
            }

            protected override void HashCore(byte[] array, int ibStart, int cbSize)
            {
                int end = ibStart + cbSize;
                for (var index = ibStart; index < end; index++)
                {
                    _hash ^= array[index];
                    _hash *= PRIME;
                }
            }

            protected override byte[] HashFinal()
            {
                return BitConverter.GetBytes(_hash);
            }
        }
    }
}