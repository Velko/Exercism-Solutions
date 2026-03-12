module Accumulate

let accumulate (func: 'a -> 'b) (input: 'a list): 'b list =

    let rec accReverseImpl func acc = function
        | [] -> acc
        | head :: tail -> accReverseImpl func (func head :: acc) tail

    let reverse input =
        accReverseImpl id [] input

    accReverseImpl func [] input |> reverse
