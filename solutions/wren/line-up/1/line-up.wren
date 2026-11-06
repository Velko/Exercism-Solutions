class LineUp {
  static format(name, number) {
    var suffix = "th"
    if ((number / 10).truncate % 10 != 1) {
      if (number % 10 == 1) suffix = "st"
      if (number % 10 == 2) suffix = "nd"
      if (number % 10 == 3) suffix = "rd"
    }
    return "%(name), you are the %(number)%(suffix) customer we serve today. Thank you!"
  }
}
