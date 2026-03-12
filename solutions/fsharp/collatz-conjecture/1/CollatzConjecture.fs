module CollatzConjecture

let steps (number: int): int option =
    
    let rec steps_internal (number: int): int =
        match number with
        | 1 -> 0
        | _ when number % 2 = 0 -> (steps_internal(number / 2)) + 1
        | _ -> (steps_internal((number * 3) + 1)) + 1

    if number <= 0 then
        None
    else
        steps_internal number |> Some