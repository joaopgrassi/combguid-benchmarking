using System;

namespace ConsoleApp
{
    public static class CombGuid
    {
        public static Guid NewCombGuid()
        {
            var originalGuid = Guid.NewGuid();
            var sourceDate = DateTime.UtcNow;

            if (originalGuid == default)
                throw new ArgumentException("Cannot create a Comb Guid from an Guid with the default value.");

            if (sourceDate.Kind != DateTimeKind.Utc)
                throw new ArgumentException("Date provided must have a Kind of Utc.");

            // Generates the Guid and populates the span
            Span<byte> guidSpan = stackalloc byte[10 + 6];

            originalGuid.TryWriteBytes(guidSpan);

            // Adds the last 6 bytes of a Utc date to the Span (will be the last portion of the Guid)
            var dateBytes = BitConverter.GetBytes(sourceDate.ToBinary()).AsSpan(2, 6);

            if (BitConverter.IsLittleEndian)
            {
                dateBytes.Reverse();
            }
            
            dateBytes.CopyTo(guidSpan.Slice(10));

            return new Guid(guidSpan);
        }
    }
}
