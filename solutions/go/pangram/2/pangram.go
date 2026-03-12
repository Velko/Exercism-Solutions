package pangram

import (
    "strings"
)

type dummy struct{}

const CharsInAlphabet = 26
func IsAsciiLetter(char rune) bool {
    return char >= 'a' && char <= 'z'
}

func IsPangram(input string) bool {
    chars_found := map[rune]dummy{}
    for _, c := range strings.ToLower(input) {
        if IsAsciiLetter(c) {
            chars_found[c] = dummy{}
        }
    }
	return len(chars_found) == CharsInAlphabet
}
