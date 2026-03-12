module QueenAttack

let create (position: int * int) =
    let x, y = position

    x >= 0 && x < 8 && y >=0 && y < 8

let canAttack (queen1: int * int) (queen2: int * int) =

    let sameColumn =
        let q1x, _ = queen1
        let q2x, _ = queen2

        q1x = q2x

    let sameRow =
        let _, q1y = queen1
        let _, q2y = queen2

        q1y = q2y

    let sameDiagonal =
        let q1x, q1y = queen1
        let q2x, q2y = queen2

        abs (q1x - q2x) = abs (q1y - q2y)

    sameColumn || sameRow || sameDiagonal