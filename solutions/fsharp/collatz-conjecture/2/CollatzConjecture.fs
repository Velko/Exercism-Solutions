module CollatzConjecture

let (|Even|Odd|) number = if number % 2 = 0 then Even else Odd
let (|Valid|Invalid|) number = if number > 0 then Valid else Invalid

let steps (number: int): int option =
    
    let rec steps_internal (number: int): int =
        match number with
        | 1 -> 0
        | Even -> steps_internal(number / 2) + 1
        | Odd -> steps_internal(number * 3 + 1) + 1

    match number with
    | Valid -> steps_internal number |> Some
    | _ -> None
        