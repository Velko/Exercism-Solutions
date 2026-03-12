using System;

public static class TelemetryBuffer
{
    public static byte[] ToBuffer(long reading)
    {
        var result = new byte[9];
    
        var (intBytes, signed) = reading switch {
            > uint.MaxValue => (BitConverter.GetBytes(reading), true),
            > int.MaxValue => (BitConverter.GetBytes((uint)reading), false),
            > ushort.MaxValue => (BitConverter.GetBytes((int)reading), true),
            >= 0 => (BitConverter.GetBytes((ushort)reading), false),
            < int.MinValue => (BitConverter.GetBytes(reading), true),
            < short.MinValue => (BitConverter.GetBytes((int)reading), true), 
            < 0  => (BitConverter.GetBytes((short)reading), true),
        };

    
        result[0] = (byte)(signed ? 256 - intBytes.Length : intBytes.Length);
        Array.Copy(intBytes, 0, result, 1, intBytes.Length);

        return result;
    }

    public static long FromBuffer(byte[] buffer)
    {
        var intBytes = buffer[1..];

        return buffer[0] switch {
        256 - 8 => BitConverter.ToInt64(intBytes),
        256 - 4 => BitConverter.ToInt32(intBytes),
        256 - 2 => BitConverter.ToInt16(intBytes),
              2 => BitConverter.ToUInt16(intBytes),
              4 => BitConverter.ToUInt32(intBytes),
        _ => 0,
        };
    }
}
