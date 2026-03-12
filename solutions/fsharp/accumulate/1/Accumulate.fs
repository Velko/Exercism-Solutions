module Accumulate

let accumulate (func: 'a -> 'b) (input: 'a list): 'b list =

    let cons head tail = head :: tail

    let rec accImpl func accfunc = function
        | [] -> accfunc []
        | head :: tail -> accImpl func (accfunc << (cons (func head))) tail

    accImpl func id input
