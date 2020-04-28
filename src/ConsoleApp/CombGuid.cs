using System;

namespace ConsoleApp
{
    public static class CombGuid
    {
        public static Guid NewCombGuid()
        {
            var originalGuid = Guid.NewGuid();
            var sourceDate = DateTime.UtcNow;

            // Creates a Span<byte> and write the Guid to it
            Span<byte> guidSpan = stackalloc byte[10 + 6];
            originalGuid.TryWriteBytes(guidSpan);

            // Get the last 6 bytes of an Utc date as a Span
            var dateBytes = BitConverter.GetBytes(sourceDate.ToBinary()).AsSpan(2, 6);

            // x86 platforms store the LEAST significant bytes first we need the opposite so SQL can order things
            if (BitConverter.IsLittleEndian)
                dateBytes.Reverse();

            dateBytes.CopyTo(guidSpan.Slice(10));

            return new Guid(guidSpan);
        }
    }
}
