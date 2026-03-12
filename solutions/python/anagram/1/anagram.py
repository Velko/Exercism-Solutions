def find_anagrams(word, candidates):
    anagrams = []

    lower_word = word.lower()
    normalized_word = _sort_chars(lower_word)

    for candidate in candidates:
        lower_candidate = candidate.lower()
        if _sort_chars(lower_candidate) == normalized_word and lower_candidate != lower_word:
            anagrams.append(candidate)

    return anagrams


def _sort_chars(lower_word):
    normalized = list(lower_word)
    normalized.sort()
    return normalized