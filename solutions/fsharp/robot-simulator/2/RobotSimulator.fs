module RobotSimulator

type Direction = North | East | South | West
type Position = int * int
type Robot = {
    direction: Direction
    position: Position
}

let create direction position =
    { direction=direction; position=position }

let move instructions robot =

    let moveStep robot step =

        let (x, y) = robot.position

        match (step, robot.direction) with

        // Turn left
        | ('L', North) -> { robot with direction = West }
        | ('L', East)  -> { robot with direction = North }
        | ('L', South) -> { robot with direction = East }
        | ('L', West)  -> { robot with direction = South }

        // Turn right
        | ('R', North) -> { robot with direction = East }
        | ('R', East)  -> { robot with direction = South }
        | ('R', South) -> { robot with direction = West }
        | ('R', West)  -> { robot with direction = North }

        // Advance
        | ('A', North) -> { robot with position = (x, y + 1)}
        | ('A', East)  -> { robot with position = (x + 1, y)}
        | ('A', South) -> { robot with position = (x, y - 1)}
        | ('A', West)  -> { robot with position = (x - 1, y)}

        | _ -> failwith "Invalid instruction"

    Seq.fold moveStep robot instructions
