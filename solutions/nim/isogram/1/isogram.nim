import std/strutils
import std/sequtils
  
proc isIsogram*(s: string): bool =
  let letters = s.toLowerAscii().filter(isLowerAscii)
  deduplicate(letters).len == letters.len
