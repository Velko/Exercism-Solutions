using System;

public static class Luhn
{
    public static bool IsValid(string number)
    {
        int checksum = 0;
        int othersum = 0;
        int validDigits = 0;
        
        foreach (char digit in number)
        {
            if (digit == ' ') continue;
            if (digit < '0' || digit > '9') return false;

            int digitValue = digit - '0';
            int doubleValue = digitValue * 2;
            if (doubleValue > 9)
                doubleValue -= 9;

            /* add digits (doubled or original) and swap the results
               between sum variables. When loop ends, _checksum_ contains
               the required sum for given number of digits */
            doubleValue += checksum;
            checksum = othersum + digitValue;
            othersum = doubleValue;

            validDigits++;
        }

        return validDigits > 1 && (checksum % 10) == 0;
    }
}