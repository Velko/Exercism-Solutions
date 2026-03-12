package thefarm

import (
    "errors"
    "fmt"
)

func DivideFood(calc FodderCalculator, numCows int) (float64, error) {
    amount, err := calc.FodderAmount(numCows)
    if err != nil {
        return 0, err
    }
    
    fat, err := calc.FatteningFactor()
    if err != nil {
        return 0, err
    }

    return amount * fat / float64(numCows), nil
}

func ValidateInputAndDivideFood(calc FodderCalculator, numCows int) (float64, error) {
    if numCows > 0 {
        return DivideFood(calc, numCows)
    } else {
        return 0, errors.New("invalid number of cows")
    }
}

type InvalidCowsError struct {
    numCows int
    message string
}

func (e *InvalidCowsError) Error() string {
	return fmt.Sprintf("%d cows are invalid: %s", e.numCows, e.message)
}


func ValidateNumberOfCows(numCows int) error {
    if numCows < 0 {
		return &InvalidCowsError {
            numCows: numCows,
            message: "there are no negative cows",
        }
    } else if numCows == 0 {
    	return &InvalidCowsError{
            numCows: numCows,
            message: "no cows don't need food",
        }
    } else {
    	return nil
    }
}
