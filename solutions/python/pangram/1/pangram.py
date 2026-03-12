import string

def is_pangram(sentence):
    chars_in_sentence = { c for c in sentence.lower() if c in string.ascii_lowercase }

    return set(string.ascii_lowercase) == chars_in_sentence
