from collections.abc import Iterable

def flatten(iterable):
    return list(_flatten(iterable))

def _flatten(value):
    if isinstance(value, Iterable):
        for sub_value in value:
            yield from _flatten(sub_value)
    elif value is not None:
        yield value
