package letter

// FreqMap records the frequency of each rune in a given text.
type FreqMap map[rune]int

// Frequency counts the frequency of each rune in a given text and returns this
// data as a FreqMap.
func Frequency(text string) FreqMap {
	frequencies := FreqMap{}
	for _, r := range text {
		frequencies[r]++
	}
	return frequencies
}

func FrequencyWorker(text string, ch chan FreqMap) {
	ch <- Frequency(text)
}

// ConcurrentFrequency counts the frequency of each rune in the given strings,
// by making use of concurrency.
func ConcurrentFrequency(texts []string) FreqMap {
	ch := make(chan FreqMap)

	for _, text := range texts {
		go FrequencyWorker(text, ch)
	}

	result := FreqMap{}
	for range texts {
		for letter, count := range <-ch {
			result[letter] += count
		}
	}

	return result
}
