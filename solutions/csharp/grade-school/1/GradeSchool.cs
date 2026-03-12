using System;
using System.Collections.Generic;
using System.Linq;

public class GradeSchool
{
    Dictionary<string, int> roster = new Dictionary<string, int>();
    private IEnumerable<(string Student, int Grade)> Data =>
        roster.Select(kv => (kv.Key, kv.Value));

    public bool Add(string student, int grade)
    {
        return roster.TryAdd(student, grade);
    }

    public IEnumerable<string> Roster()
    {
        return Data
            .OrderBy(r => r.Grade)
            .ThenBy(r => r.Student)
            .Select(r => r.Student);
    }

    public IEnumerable<string> Grade(int grade)
    {
        return Data
            .Where(r => r.Grade == grade)
            .OrderBy(r => r.Student)
            .Select(r => r.Student);
    }
}