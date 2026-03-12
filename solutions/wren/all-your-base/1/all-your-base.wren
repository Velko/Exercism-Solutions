class AllYourBase {
  static rebase(inputBase, digits, outputBase) {
    if (inputBase < 2) Fiber.abort("input base must be >= 2")
    if (digits.any {|d| d < 0 || d >= inputBase} ) Fiber.abort("all digits must satisfy 0 <= d < input base")
    if (outputBase < 2) Fiber.abort("output base must be >= 2")
    
    var number = digits.reduce(0) { |num, digit| num * inputBase + digit }

    if (number == 0) return [0]

    var outDigits = []
    while (number > 0) {
      outDigits.insert(0, number % outputBase)
      number = (number / outputBase).truncate
    }

    return outDigits
  }
}
