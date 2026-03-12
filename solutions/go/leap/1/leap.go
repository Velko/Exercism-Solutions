// Functions to determine leap years
package leap

// IsLeapYear determines if given year is a leap year in Gregorian calendar
func IsLeapYear(year int) bool {
	return IsDivisibleBy(year, 4) && (!IsDivisibleBy(year, 100) || IsDivisibleBy(year, 400))
}

// Convenience function to determine if number is divisible by another number
func IsDivisibleBy(dividend int, divisor int) bool {
    return dividend % divisor == 0;
}