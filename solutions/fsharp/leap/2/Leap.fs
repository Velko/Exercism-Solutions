module Leap

let leapYear (year: int): bool =

    let isDivisibleBy (number: int ) (divisor: int): bool =
        number % divisor = 0

    isDivisibleBy year 400 || isDivisibleBy year 4 && not (isDivisibleBy year 100)
