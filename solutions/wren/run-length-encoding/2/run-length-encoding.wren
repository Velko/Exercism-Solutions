class RLE {
  static encode(s) {
    if (s == "") return ""

    var parts = []

    var old_char = s[0]
    var run_count = 0
    for (c in s) {
      if (old_char == c) {
        run_count = run_count + 1
      } else {
        parts.add(run_count == 1 ? old_char : "%(run_count)%(old_char)")
        old_char = c
        run_count = 1
      }
    }

    parts.add(run_count == 1 ? old_char : "%(run_count)%(old_char)")
    
    return parts.join()
  }

  
  static decode(s) {
    var parts = []

    var count = 0
    for (c in s) {
      var digit = asDigit(c)
      if (!(digit is Null)) {
        count = count * 10 + digit
      } else {
        if (count == 0) {
          parts.add(c)
        } else {
          parts.addAll(List.filled(count, c))
        }
        count = 0
      }
    }

    return parts.join()
  }

  static asDigit(c) {
    var b = c.bytes[0]
    if (b >= 0x30 && b <= 0x39) {
        return b - 0x30
    }
  }
}
