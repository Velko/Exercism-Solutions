def classify(number):
    """ A perfect number equals the sum of its positive divisors.

    :param number: int a positive integer
    :return: str the classification of the input integer
    """

    if number < 1:
        raise ValueError("Classification is only possible for positive integers.")

    aliquot_sum = sum(filter(lambda n: number % n == 0, range(1, number)))

    if aliquot_sum == number:
        return "perfect"

    if aliquot_sum > number:
        return "abundant"

    return "deficient"
