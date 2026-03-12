"""Word Count on Exercism's Python Track."""

import re
from itertools import groupby

def count_words(sentence):
    """Count the occurrences of each _word_ `sentence`."""

    # Extract all words that consists from letters, digits or apostrophes
    words = re.findall(r"([a-z0-9']+)*", sentence.lower())
    words = map(lambda s: s.strip("'"), words) # strip apostrophes from beginning/end
    words = filter(lambda s: s != "", words)   # ignore empty "words"

    # collect into groups and count items in each of them
    groups = groupby(sorted(words))
    word_counts = map(lambda k_v: (k_v[0], len(list(k_v[1]))), groups)

    # convert to expected output format
    return dict(word_counts)
