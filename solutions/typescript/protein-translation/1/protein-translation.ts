export function translate(strand: string) {
    return [...split_strand(strand)];
}

function* split_strand(strand: string) {
    for (let s = 0; s < strand.length; s += 3)
    {
        let rna = strand.slice(s, s + 3);

        switch (rna) {
            case "AUG":
                yield "Methionine"
                break;
            case "UUU":
            case "UUC":
                yield "Phenylalanine";
                break;
            case "UUA":
            case "UUG":
                yield "Leucine";
                break;
            case "UCU":
            case "UCC":
            case "UCA":
            case "UCG":
                yield "Serine";
                break;
            case "UAU":
            case "UAC":
                yield "Tyrosine";
                break;
            case "UGU":
            case "UGC":
                yield "Cysteine";
                break;
            case "UGG":
                yield "Tryptophan";
                break;
            case "UAA":
            case "UAG":
            case "UGA":
                return;
            default:
                throw new Error("Invalid codon");
        }
    }
}