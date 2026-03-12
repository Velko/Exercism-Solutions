import std/sets

proc sum*(limit: int, factors: openArray[int]): int =
    var uniqueMultiples = initHashSet[int]()
    for factor in factors:
        if factor == 0: continue
        var multiple = factor
        while multiple < limit:
            uniqueMultiples.incl(multiple)
            multiple += factor

    for multiple in uniqueMultiples:
        result += multiple
