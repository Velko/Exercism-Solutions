DNA2RNA = {
    "G": "C",
    "C": "G",
    "T": "A",
    "A": "U",
}

def to_rna(dna_strand):
    return "".join(map(lambda dna: DNA2RNA[dna], dna_strand))
