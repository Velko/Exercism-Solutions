type
  Allergen* = enum
    Eggs, Peanuts, Shellfish, Strawberries, Tomatoes, Chocolate, Pollen, Cats

proc isAllergicTo*(score: int, allergen: Allergen): bool =
  (score and ( 1 shl ord(allergen))) != 0

proc allergies*(score: int): set[Allergen] =
  var res: set[Allergen] = {}
  for allergen in low(Allergen)..high(Allergen):
    if isAllergicTo(score, allergen):
      res = res + { allergen }
  res
