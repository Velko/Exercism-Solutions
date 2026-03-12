module Hamming

let distance (strand1: string) (strand2: string): int option =
    if strand1.Length = strand2.Length then
        (strand1, strand2) 
        ||> Seq.map2 (fun x y -> x <> y)
        |> Seq.where (fun b -> b)
        |> Seq.length
        |> Some
    else
        None
    