using System;
using System.Collections.Generic;
using System.Linq;

public static class VariableLengthQuantity
{
    public static uint[] Encode(uint[] numbers)
        => numbers.SelectMany(EncodeSingle).ToArray();

    private static readonly uint[] ENCODED_ZERO = { 0 };

    private static uint[] EncodeSingle(uint num)
    {
        if (num == 0) return ENCODED_ZERO;

        const int BYTES_PER_UINT_ENC = 5;
        Span<uint> bytes = stackalloc uint[BYTES_PER_UINT_ENC];

        int start;
        for (start = BYTES_PER_UINT_ENC; num > 0; num >>= 7)
            bytes[--start] = num & 0x7F;

        for (int i = start; i < BYTES_PER_UINT_ENC - 1; ++i)
            bytes[i] |= 0x80;

        return bytes[start..].ToArray();
    }

    public static uint[] Decode(uint[] bytes)
    {
        IEnumerable<uint> DecodeInternal()
        {
            uint? decoded = null;
            foreach (var b in bytes) {
                decoded = (decoded << 7) ?? 0;
                decoded |= b & 0x7F;
                if ((b & 0x80) == 0)
                {
                    yield return decoded.Value;
                    decoded = null;
                }
            }

            if (decoded.HasValue)
                throw new InvalidOperationException();
        }

        return DecodeInternal().ToArray();
    }
}