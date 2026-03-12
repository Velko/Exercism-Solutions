proc isDivisibleBy(number: int, divisor: int): bool =
    return number mod divisor == 0
  
proc isLeapYear*(year: int): bool =
    return year.isDivisibleBy(4) and 
     (not year.isDivisibleBy(100) or
          year.isDivisibleBy(400)
    )
