class Year {
  static isLeap(year) {
    return isDivisibleBy(year, 4) && 
      (!isDivisibleBy(year, 100) || isDivisibleBy(year, 400))
  }

  static isDivisibleBy(dividend, divisor) {
    return dividend % divisor == 0
  }
}
