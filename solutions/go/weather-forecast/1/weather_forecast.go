// Package weather provides something to do something.
package weather

// CurrentCondition stores current condition.
var CurrentCondition string

// CurrentLocation stores current location.
var CurrentLocation string

// Forecast calculates forecast.
func Forecast(city, condition string) string {
	CurrentLocation, CurrentCondition = city, condition
	return CurrentLocation + " - current weather condition: " + CurrentCondition
}
