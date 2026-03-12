"""Transforms Scrabble scores from legacy system to new one."""

from itertools import repeat, chain

def transform(legacy_data):
    """Transforms Scrabble scores.

    :param legacy_data: dict[int, list[char]] - scores in legacy format
    :return: dict[char, int] - scores in new format
    """
    chars_with_scores = map(_process_item, legacy_data.items())
    return dict(chain(*chars_with_scores))

def _process_item(score_characters):
    score, characters = score_characters

    lowercase_characters = map(lambda char: char.lower(), characters)
    scores_for_each = repeat(score)

    return zip(lowercase_characters, scores_for_each)
