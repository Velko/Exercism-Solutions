import std/strutils
import std/sequtils
  
proc hey*(s: string): string =
    let heyBob = s.strip()
    if heyBob == "":
        return "Fine. Be that way!"
    let isQuestion = heyBob[^1] ==  '?'
    let hasLetters = heyBob.any(isAlphaAscii)
    let isShouting = heyBob.toUpperAscii() == heyBob and hasLetters
    if isQuestion:
        if isShouting:
            return "Calm down, I know what I'm doing!"
        return "Sure."
    if isShouting:
        return "Whoa, chill out!"
    return "Whatever."
