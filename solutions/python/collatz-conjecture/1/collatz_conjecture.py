def steps(number):
    if number <= 0:
        raise ValueError("Only positive integers are allowed")

    if number == 1:
        return 0

    if number & 1 == 0:
        return steps(number // 2) + 1

    return steps(number * 3 + 1) + 1
