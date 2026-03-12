module GuessingGame

let reply (guess: int): string =
    match guess with
    | 42 -> "Correct"
    | g when abs(g-42) <= 1 -> "So close"
    | g when g < 42 -> "Too low"
    | _ -> "Too high"