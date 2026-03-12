module Raindrops

let convert (number: int): string =

    let (|IsFactorOf|_|) divisor num =
        if num % divisor = 0 then Some(num / divisor) else None

    let rec factor (num: int): Set<int> =
        match num with
        | IsFactorOf 3 div -> (factor div).Add(3)
        | IsFactorOf 5 div -> (factor div).Add(5)
        | IsFactorOf 7 div -> (factor div).Add(7)
        | _ -> Set.empty

    let factorToSound (f: int) =
        match f with
        | 3 -> "Pling"
        | 5 -> "Plang"
        | 7 -> "Plong"
        | _ -> failwith "Unsupported factor."

    let factored = factor number

    if Set.isEmpty factored then
        string number
    else
        factored
        |> Set.toSeq
        |> Seq.sort
        |> Seq.map factorToSound
        |> String.concat ""
