import std/algorithm
import std/sequtils
import std/sugar
  
type
  Student* = object
    name*: string
    grade*: int

  School* = object
    students*: seq[Student]

proc studentCmp(x, y: Student): int =
  result = cmp(x.grade, y.grade)
  if result == 0:
    result = cmp(x.name, y.name)

proc roster*(school: School): seq[string] =
  ## Returns the names of every student in the `school`, sorted by grade then name.
  result = school.students
    .sorted(studentCmp)
    .map(s => s.name)
    .toSeq

proc addStudent*(school: var School, name: string, grade: int) =
  ## Adds a student with `name` and `grade` to the `school`.
  ##
  ## Raises a `ValueError` if `school` already contains a student named `name`.
  if school.students.any(s => s.name == name):
    raise newException(ValueError, name)
  school.students.add(Student(name: name, grade: grade))

proc grade*(school: School, grade: int): seq[string] =
  ## Returns the names of the students in the given `school` and `grade`, in
  ## alphabetical order.
  result = school.students
    .filter(g => g.grade == grade)
    .map(s => s.name)
    .sorted()
    .toSeq
