class Number {
  static isArmstrong(n) {
    var digits = n.toString
    var len = digits.count
    var sumOfDigits = 0
    for (d in digits) {
      sumOfDigits = sumOfDigits + Num.fromString(d).pow(len)
    }
   
    return n == sumOfDigits
  }
}