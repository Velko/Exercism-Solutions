"""Sum of Multiples exercise on Exercism's Python Track"""

from itertools import chain

def sum_of_multiples(limit, factors):
    """Add up all the unique multiples of the factors that are less than the limit.
    :param limit: int - upper limit of the multiples
    :param factors: list[int] - list of factors
    :return: int - the resulting sum
    """

    nonzero_factors = filter(lambda f: f != 0, factors)
    factor_multiples = map(lambda factor: range(factor, limit, factor), nonzero_factors)
    all_multiples = chain(*factor_multiples)
    unique_multiples = set(all_multiples)

    return sum(unique_multiples)
