from collections.abc import Iterable

def flatten(iterable):
    return list(_flatten(iterable))

def _flatten(value):
    if isinstance(value, Iterable):
        for sub_value in value:
            for item in _flatten(sub_value):
                yield item
    elif value is not None:
        yield value
