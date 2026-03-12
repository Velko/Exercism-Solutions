module RobotSimulator

type Direction = North | East | South | West
type Position = int * int

let create direction position =
    (direction, position)

let rec move instructions robot =

    let (|Instruction|_|) p s =
        if s <> "" && s[0] = p then
            Some(s[1..])
        else
            None

    let (direction, position) = robot
    let (x, y) = position

    match (instructions, direction) with

    // Turn left
    | (Instruction 'L' next, North) -> move next (West, position)
    | (Instruction 'L' next, East)  -> move next (North, position)
    | (Instruction 'L' next, South) -> move next (East, position)
    | (Instruction 'L' next, West)  -> move next (South, position)

    // Turn right
    | (Instruction 'R' next, North) -> move next (East, position)
    | (Instruction 'R' next, East)  -> move next (South, position)
    | (Instruction 'R' next, South) -> move next (West, position)
    | (Instruction 'R' next, West)  -> move next (North, position)

    // Advance
    | (Instruction 'A' next, North) -> move next (direction, (x, y + 1))
    | (Instruction 'A' next, East)  -> move next (direction, (x + 1, y))
    | (Instruction 'A' next, South) -> move next (direction, (x, y - 1))
    | (Instruction 'A' next, West)  -> move next (direction, (x - 1, y))

    // Arrived - out of instructions
    | ("", _) -> robot

    | _ -> failwith "Invalid instruction"

