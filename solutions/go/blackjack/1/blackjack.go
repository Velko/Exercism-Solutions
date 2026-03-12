package blackjack

// ParseCard returns the integer value of a card following blackjack ruleset.
func ParseCard(card string) int {
    switch card {
    case "ace":
    	return 11
    case "two":
    	return 2
    case "three":
    	return 3
    case "four":
    	return 4
    case "five":
    	return 5
    case "six":
    	return 6
    case "seven":
    	return 7
    case "eight":
    	return 8
    case "nine":
    	return 9
    case "ten":
    	fallthrough
    case "jack":
    	fallthrough
    case "queen":
    	fallthrough
    case "king":
    	return 10
    }
	return 0
}

// FirstTurn returns the decision for the first turn, given two cards of the
// player and one card of the dealer.
func FirstTurn(card1, card2, dealerCard string) string {

    var playerSum = ParseCard(card1) + ParseCard(card2)
    var dealerSum = ParseCard(dealerCard)
    
    switch {
    case playerSum == 22:
    	return "P"
    case playerSum == 21 && dealerSum < 10:
    	return "W"
    case playerSum == 21:
    	return "S"
    case playerSum >= 17 && playerSum <= 20:
    	return "S"
    case playerSum >= 12 && playerSum <= 16 && dealerSum >= 7:
    	return "H"
    case playerSum >= 12 && playerSum <= 16:
    	return "S"
    }
	return "H"
}
