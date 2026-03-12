def leap_year(year):
    year = IntExt(year)
    
    return year.is_divisible_by(4) \
        and (  year.not_divisible_by(100) \
            or year.is_divisible_by(400))

class IntExt:
    def __init__(self, number):
        self.number = number
        
    def is_divisible_by(self, divisor):
        return self.number % divisor == 0
    
    def not_divisible_by(self, divisor):
        return self.number % divisor != 0