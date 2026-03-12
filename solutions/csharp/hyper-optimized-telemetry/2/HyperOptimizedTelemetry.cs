using System;

public static class TelemetryBuffer
{
    public static byte[] ToBuffer(long reading)
    {  
        var (intBytes, signed) = reading switch {
            > uint.MaxValue or 
            < int.MinValue => (BitConverter.GetBytes(reading), true),

            > int.MaxValue => (BitConverter.GetBytes((uint)reading), false),

            > ushort.MaxValue or 
            < short.MinValue => (BitConverter.GetBytes((int)reading), true),

            >= 0 => (BitConverter.GetBytes((ushort)reading), false),
            < 0  => (BitConverter.GetBytes((short)reading), true),
        };

        var result = new byte[9];
        result[0] = (byte)(signed ? -intBytes.Length : intBytes.Length);
        intBytes.CopyTo(result, 1);

        return result;
    }

    public static long FromBuffer(byte[] buffer)
    {
        var intBytes = buffer[1..];

        return (sbyte)buffer[0] switch {
        - 8 => BitConverter.ToInt64(intBytes),
        - 4 => BitConverter.ToInt32(intBytes),
        - 2 => BitConverter.ToInt16(intBytes),
          2 => BitConverter.ToUInt16(intBytes),
          4 => BitConverter.ToUInt32(intBytes),
        _ => 0,
        };
    }
}
