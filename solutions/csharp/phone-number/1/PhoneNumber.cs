using System;
using System.Linq;

public class PhoneNumber
{
    public static string Clean(string phoneNumber)
    {
        var cleaned = new string(
            phoneNumber
                .Where(Char.IsDigit)
                .ToArray()
        );

        if (cleaned.Length == 11 && cleaned[0] == '1')
            cleaned = cleaned.Substring(1);

        if (cleaned.Length != 10
            || cleaned[0] is not >= '2' and <= '9'
            || cleaned[3] is not >= '2' and <= '9')
            throw new ArgumentException();

        return cleaned;
    }
}