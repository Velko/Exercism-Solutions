from string import ascii_lowercase

def is_isogram(string):
    chars_only = [c for c in string.lower() if c in ascii_lowercase]

    return len(set(chars_only)) == len(chars_only)
