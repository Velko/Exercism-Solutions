module BirdWatcher

let lastWeek: int[] =
   [| 0; 2; 5; 3; 7; 8; 4 |]

let yesterday(counts: int[]): int =
    let len = counts |> Array.length
    counts[len-2]

let total(counts: int[]): int =
    Array.sum counts

let dayWithoutBirds(counts: int[]): bool =
    Array.contains 0 counts

let incrementTodaysCount(counts: int[]): int[] =
    let len = Array.length counts
    counts.[len-1] <- (counts.[len-1] + 1)
    counts

let oddWeek(counts: int[]): bool =
    match counts with
    | [| _ ; 0 ; _ ; 0 ; _ ; 0 ; _ |] -> true
    | [| _ ; 10 ; _ ; 10 ; _ ; 10 ; _ |] -> true
    | [| 5 ; _ ; 5 ; _ ; 5 ; _ ; 5|] -> true
    | _ -> false
