module Pangram
open System

let isPangram (input: string): bool =
    input.ToLower()
    |> Seq.where Char.IsLetter
    |> Set.ofSeq
    |> Set.count
    |> (=) 26
