module Yacht

type Category = 
    | Ones
    | Twos
    | Threes
    | Fours
    | Fives
    | Sixes
    | FullHouse
    | FourOfAKind
    | LittleStraight
    | BigStraight
    | Choice
    | Yacht

type Die =
    | One 
    | Two 
    | Three
    | Four 
    | Five 
    | Six

let score category dice =

    let dieValue die =
        match die with
        | Die.One -> 1
        | Die.Two -> 2
        | Die.Three -> 3
        | Die.Four -> 4
        | Die.Five -> 5
        | Die.Six -> 6

    let sumValues die dice =
        dice
        |> Seq.filter (fun d -> d = die)
        |> Seq.sumBy dieValue

    let sortedDice dice = 
        dice
        |> Seq.sortBy (fun v -> dieValue v)
        |> Seq.toList

    let groupCount dice =
        dice
        |> Seq.groupBy (fun d -> d)
        |> Seq.map (fun (die, lst) -> (die, Seq.length lst))

    let (|Has4OrMore|_|) dice = 
        dice
        |> groupCount
        |> Seq.filter (fun (_, cnt) -> cnt >= 4)
        |> Seq.tryExactlyOne

    let HasFullHouse dice =
        let counts =
            dice
            |> groupCount
            |> Seq.map (fun (_, n) -> n)
            |> Seq.sort
            |> Seq.toList

        counts = [2; 3]

    match (category, dice) with
    | (Category.Yacht, _) when Seq.length (Seq.distinct dice) = 1 -> 50
    | (Category.LittleStraight, _) when sortedDice dice = [Die.One; Die.Two; Die.Three; Die.Four; Die.Five] -> 30
    | (Category.BigStraight, _) when sortedDice dice = [Die.Two; Die.Three; Die.Four; Die.Five; Die.Six] -> 30
    | (Category.Choice, _) -> Seq.sumBy dieValue dice
    | (Category.Ones, _) -> sumValues Die.One dice
    | (Category.Twos, _) -> sumValues Die.Two dice
    | (Category.Threes, _) -> sumValues Die.Three dice
    | (Category.Fours, _) -> sumValues Die.Four dice
    | (Category.Fives, _) -> sumValues Die.Five dice
    | (Category.Sixes, _) -> sumValues Die.Six dice
    | (Category.FourOfAKind, Has4OrMore (die, _)) -> (dieValue die) * 4
    | (Category.FullHouse, _) when HasFullHouse dice -> Seq.sumBy dieValue dice
    | _ -> 0
