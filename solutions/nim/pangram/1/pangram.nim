import std/strutils
import std/sequtils
import std/sets
  
proc isPangram*(s: string): bool =
  result = s
    .toUpperAscii()
    .filter(isAlphaAscii)
    .toHashSet()
    .len == UppercaseLetters.len
