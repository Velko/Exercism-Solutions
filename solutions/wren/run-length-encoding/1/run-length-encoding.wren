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
        if (run_count == 1) {
          parts.add(old_char)
        } else {
          parts.add("%(run_count)%(old_char)")          
        }
        old_char = c
        run_count = 1
      }
    }

    if (run_count == 1) {
      parts.add(old_char)
    } else {
      parts.add("%(run_count)%(old_char)")          
    }
    
    return parts.join()
  }

  
  static decode(s) {
    if (s == "") return ""

    var parts = []

    var count = 0
    for (c in s) {
      var b = c.bytes[0]
      if (b >= 0x30 && b <= 0x39) {
        count = count * 10 + b - 0x30
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
}
