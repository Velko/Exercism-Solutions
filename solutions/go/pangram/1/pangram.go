package pangram

import (
    "unicode"
    "strings"
)

func IsPangram(input string) bool {
    counts := map[rune]int{}
    for _, c := range strings.ToLower(input) {
        if unicode.IsLetter(c) {
            counts[c]++
        }
    }
	return len(counts) == 26
}
