def is_armstrong_number(number):
    number_str = str(number)
    num_digits = len(number_str)

    total = sum(map(lambda d: int(d) ** num_digits, number_str))

    return number == total
