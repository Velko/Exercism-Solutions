"""Largest Series Product exercise on Exercism's Python Track."""

from functools import reduce
from operator import mul

def largest_product(series, size):
    """Calculate the largest product for a contiguous substring of digits of length `size`."""

    if size < 0:
        raise ValueError("span must not be negative")

    if size > len(series):
        raise ValueError("span must not exceed string length")

    try:
        series_num = list(map(int, series))
    except ValueError as err:
        raise ValueError("digits input must only contain digits") from err

    return max(_calculate_products(series_num, size))

def _calculate_products(series_num, size):
    for start in range(len(series_num) - size + 1):
        product = reduce(mul, series_num[start: start+size], 1)
        yield product
