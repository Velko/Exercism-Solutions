import string

def is_valid(isbn):
    digits = list(_parse_isbn(isbn))

    if len(digits) != 10:
        return False

    if 10 in digits and digits.index(10) != 9:
        return False

    checksum = sum([(10-i) * d for i, d in enumerate(digits)])

    return checksum % 11 == 0


def _parse_isbn(isbn):
    for c in isbn:
        if c in string.digits:
            yield int(c)
        elif c == 'X':
            yield 10
        elif c != '-':
            return

