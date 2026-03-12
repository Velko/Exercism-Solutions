def square_of_sum(number):
    return sum(range(1, number + 1)) ** 2


def sum_of_squares(number):
    return sum(map(lambda n: n * n, range(1, number + 1)))


# square_of_sum is actually sum_of_squares + "something". That "something" is exactly what we need to calculate

# (a+b) * (a+b) => a*a + a*b + b*a + b*b = a*a + 2*a*b + b*b

# (a+b+c) * (a+b+c)  => (a*a + a*b + a*c) + (a*b + b*b + b*c) + (a*c + b*c + c*c) => (a*a + b*b + c*c ) + (2*a*b + 2*a*c + 2*b*c)

# (a+b+c+d)*(a+b+c+d) => (a*a + a*b + a*c + a*d) + (b*a + b*b + b*c + b*d) + (c*a + c*b + c*c + c*d) + (d*a + d*b + d*c + d*d)
#                     => (a*a + a*b + a*c + a*d) + (a*b + b*b + b*c + b*d) + (a*c + b*c + c*c + c*d) + (a*d + b*d + c*d + d*d)
#                     => (2*a*b + 2*a*c + 2*a*d + 2*b*c + 2*b*d + 2*c*d) + (a*a + b*b + c*c + d*d)
#                     => 2 * (a*b + a*c + a*d + b*c + b*d + c*d) + (a*a + b*b + c*c + d*d)
#                     => 2 * (a * ( b + c + d) + b * (c + d) + c * (d)) + (a*a + b*b + c*c + d*d)

def difference_of_squares(number):
    return sum(map(lambda a: a * sum(range(a + 1, number + 1)), range(1, number + 1))) * 2




