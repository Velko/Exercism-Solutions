using System;
using System.Globalization;

public static class HighSchoolSweethearts
{
    public static string DisplaySingleLine(string studentA, string studentB)
    {
        return $"                  {studentA} ♡ {studentB}                    ";
    }

    public static string DisplayBanner(string studentA, string studentB)
    {
        var template =
@"
     ******       ******
   **      **   **      **
 **         ** **         **
**            *            **
**                         **
**     {0} +  {1}    **
 **                       **
   **                   **
     **               **
       **           **
         **       **
           **   **
             ***
              *
";

        return string.Format(template, studentA, studentB);
    }

    public static string DisplayGermanExchangeStudents(string studentA
        , string studentB, DateTime start, float hours)
    {
        return string.Format(new CultureInfo("de-DE"), "{0} and {1} have been dating since {2:d} - that's {3:N2} hours", studentA, studentB, start, hours);
    }
}
