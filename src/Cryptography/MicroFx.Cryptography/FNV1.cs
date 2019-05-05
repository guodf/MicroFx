using System;
using System.Security.Cryptography;

namespace System.Security.Cryptography
{
    public enum FNVBits
    {
        Bits32,
        Bits64
    }
    public abstract class FNV1 : HashAlgorithm
    {
        public static new FNV1 Create()
        {
            return Create(FNVBits.Bits32);
        }

        public static FNV1 Create(FNVBits fNVBits)
        {
            FNV1 fnv1=null;
            switch (fNVBits)
            {
                case FNVBits.Bits32:
                    fnv1 = new Implementation32();
                    break;
                case FNVBits.Bits64:
                    fnv1 = new Implementation64();
                    break;
            }
            return fnv1;
        }

        public static new FNV1 Create(string hashName)
        {
            throw new NotSupportedException();
        }

        private sealed class Implementation32 : FNV1
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
                    _hash *= PRIME;
                    _hash ^= array[index];
                }
            }

            protected override byte[] HashFinal()
            {
                return BitConverter.GetBytes(_hash);
            }
        }

        private sealed class Implementation64 : FNV1
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
                    _hash *= PRIME;
                    _hash ^= array[index];
                }
            }

            protected override byte[] HashFinal()
            {
                return BitConverter.GetBytes(_hash);
            }
        }
    }
}