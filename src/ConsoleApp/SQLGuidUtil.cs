﻿using System;
using System.Runtime.InteropServices;

namespace ConsoleApp
{
    /// <summary>
    /// Copied from https://blogs.msdn.microsoft.com/dbrowne/2012/07/03/how-to-generate-sequential-guids-for-sql-server-in-net/
    /// </summary>
    public static class SQLGuidUtil
    {
        [DllImport("rpcrt4.dll", SetLastError = true)]
        static extern int UuidCreateSequential(out Guid guid);

        public static Guid NewSequentialId()
        {
            UuidCreateSequential(out Guid guid);

            var s = guid.ToByteArray();
            var t = new byte[16];

            t[3] = s[0];
            t[2] = s[1];
            t[1] = s[2];
            t[0] = s[3];
            t[5] = s[4];
            t[4] = s[5];
            t[7] = s[6];
            t[6] = s[7];
            t[8] = s[8];
            t[9] = s[9];
            t[10] = s[10];
            t[11] = s[11];
            t[12] = s[12];
            t[13] = s[13];
            t[14] = s[14];
            t[15] = s[15];

            return new Guid(t);
        }
    }
}
