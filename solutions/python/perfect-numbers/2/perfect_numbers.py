import math

def classify(number):
    """ A perfect number equals the sum of its positive divisors.

    :param number: int a positive integer
    :return: str the classification of the input integer
    """

    if number < 1:
        raise ValueError("Classification is only possible for positive integers.")

    # edge case
    if number == 1:
        return "deficient"

    root = int(math.sqrt(number))

    aliquot_sum = 1 # we're starting with 2, but 1 is a valid factor
    for factor in range(2, root + 1):
        complementary, reminder = divmod(number, factor)
        if reminder == 0:
            aliquot_sum += factor

            if complementary != factor:
                aliquot_sum += complementary

    if aliquot_sum == number:
        return "perfect"

    if aliquot_sum > number:
        return "abundant"

    return "deficient"



# Example 1, factoring 80:
# 1, 2, 4, 5, 8, 10, 16, 20, 40

# sqrt(80):  8.9  ... -> 8

# 1 * 80
# 2 * 40
# 4 * 20
# 5 * 16
# 8 * 10   <--- sqrt rounded down
# 10 * 8
# 20 * 4
# 40 * 2
# 80 * 1
#------------------------------
# so the factors after the sqrt are symmetric, we can optimize by looping
# up to the sqrt and also adding the complementary one

# Example 2, factoring 81:
# 1, 2, 9, 27, 81

# sqrt(81):  9

# 1 * 81
# 3 * 27
# 9 * 9     <--- sqrt
# 27 * 3
# 81 * 1
#--------------------------
# symmetric again, but if sqrt is exact integer, it should count it only once