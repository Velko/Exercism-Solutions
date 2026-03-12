import std/strutils
import std/sequtils
import std/sugar
  
proc abbreviate*(s: string): string =
  s.split({' ', '-', '_'})
   .filter(w => w != "")
   .map(w => w[0])
   .join()
   .toUpperAscii()
