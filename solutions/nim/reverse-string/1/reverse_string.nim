proc reverse*(s: string): string =
  result = ""
  for c in s:
    result = c & result
