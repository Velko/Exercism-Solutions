import std/sequtils
import std/sugar
  
proc distance*(a, b: string): int =
  if a.len != b.len:
    raise newException(ValueError, "Strands must be of equal length.")
  result = zip(a, b)
    .filter(ab => ab[0] != ab[1])
    .len
