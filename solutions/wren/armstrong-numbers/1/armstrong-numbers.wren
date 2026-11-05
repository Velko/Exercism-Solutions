class Number {
  static isArmstrong(n) {
    var digits = n.toString
    var sumOfDigits = 0
    for (d in digits) {
      var dNum = Num.fromString(d)
      var dToPower = 1
      for (x in 1 .. digits.count) {
        dToPower = dToPower * dNum
      }
      sumOfDigits = sumOfDigits + dToPower
    }
   
    return n == sumOfDigits
  }
}