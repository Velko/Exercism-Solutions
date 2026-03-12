def find_anagrams(word, candidates):
    anagrams = []

    lower_word = word.lower()
    normalized_word = sorted(lower_word)

    for candidate in candidates:
        lower_candidate = candidate.lower()
        if sorted(lower_candidate) == normalized_word and lower_candidate != lower_word:
            anagrams.append(candidate)

    return anagrams
