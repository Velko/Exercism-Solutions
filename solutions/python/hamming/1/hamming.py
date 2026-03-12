def distance(strand_a, strand_b):
    if len(strand_a) != len(strand_b):
        raise ValueError("Strands must be of equal length.")

    return len(list(filter(lambda ab: ab[0] != ab[1], zip(strand_a, strand_b))))
