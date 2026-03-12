module MatchingBrackets

type Bracket =
    | Square
    | Curly
    | Round

let (|Opening|_|) = function
    | '[' -> Some(Square)
    | '{' -> Some(Curly)
    | '(' -> Some(Round)
    | _ -> None

let (|Closing|_|) = function
    | ']' -> Some(Square)
    | '}' -> Some(Curly)
    | ')' -> Some(Round)
    | _ -> None

let rec findMatching input expect =
    match input with
    | [] -> List.isEmpty expect
    | Opening(bracket) :: tail -> findMatching tail (bracket :: expect)
    | Closing(bracket) :: tail -> (List.tryHead expect) = Some(bracket) && findMatching tail (List.tail expect)
    | _ :: tail -> findMatching tail expect

let isPaired (input: string) =
    findMatching (List.ofSeq input) []
