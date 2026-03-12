module Pangram

let isPangram (input: string): bool =
    input.ToLower()
    |> Seq.toList
    |> Seq.where (fun c -> System.Char.IsLetter(c))
    |> Set.ofSeq
    |> Set.count = 26
