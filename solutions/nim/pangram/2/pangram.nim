import std/strutils
import std/sequtils
  
proc isPangram*(s: string): bool =
  result = s
    .toUpperAscii()
    .filter(isAlphaAscii)
    .deduplicate()
    .len == UppercaseLetters.len
