module ProteinTranslation

let proteins rna =

    let codonStartIndices (rna: string) = 
        seq { 0 .. 3 .. rna.Length - 1 }

    let extractCodon (rna: string) index =
        rna[index .. index + 2]

    let codonToProtein codon =
        match codon with
        | "AUG"                         -> Some("Methionine")
        | "UUU" | "UUC"                 -> Some("Phenylalanine")
        | "UUA" | "UUG"                 -> Some("Leucine")
        | "UCU" | "UCC" | "UCA" | "UCG" -> Some("Serine")
        | "UAU" | "UAC"                 -> Some("Tyrosine")
        | "UGU" | "UGC"                 -> Some("Cysteine")
        | "UGG"                         -> Some("Tryptophan")
        | "UAA" | "UAG" | "UGA"         -> None
        | _ -> failwith "Invalid codon"

    rna
    |> codonStartIndices
    |> Seq.map (fun index -> extractCodon rna index)
    |> Seq.map codonToProtein
    |> Seq.takeWhile (fun protein -> protein.IsSome)
    |> Seq.choose id
    |> Seq.toList
