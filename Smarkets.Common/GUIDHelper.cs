using System;
using System.Collections.Generic;
using System.Text;

namespace Smarkets.Common
{
    public class GUIDHelper
    {
        public static long FromGUIDToLong(Guid input)
        {
            byte[] buffer = input.ToByteArray();

            long l = BitConverter.ToInt64(buffer, 0);
            return l;
        }

        public static Guid ToGUID(long input)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(input).CopyTo(bytes, 0);
            return new Guid(bytes);
        }
    }
}
