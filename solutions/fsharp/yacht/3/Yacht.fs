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

    let (|IsYacht|_|) dice =
        if Seq.length (Seq.distinct dice) = 1 then Some() else None

    let dieScore die =
        match die with
        | Die.One -> 1
        | Die.Two -> 2
        | Die.Three -> 3
        | Die.Four -> 4
        | Die.Five -> 5
        | Die.Six -> 6

    let (|NumberOfDies|) die dice =
        dice
        |> Seq.filter (fun d -> d = die)
        |> Seq.length

    let sortedDice dice = 
        dice
        |> Seq.sortBy (fun v -> dieScore v)
        |> Seq.toList

    let (|IsLittleStraight|_|) dice =
        if sortedDice dice = [Die.One; Die.Two; Die.Three; Die.Four; Die.Five] then Some() else None

    let (|IsBigStraight|_|) dice =
        if sortedDice dice = [Die.Two; Die.Three; Die.Four; Die.Five; Die.Six] then Some() else None

    let groupCount dice =
        dice
        |> Seq.groupBy (fun d -> d)
        |> Seq.map (fun (die, lst) -> (die, Seq.length lst))

    let (|Has4OrMore|_|) dice = 
        dice
        |> groupCount
        |> Seq.filter (fun (_, cnt) -> cnt >= 4)
        |> Seq.tryExactlyOne

    let (|HasFullHouse|_|) dice =
        let counts =
            dice
            |> groupCount
            |> Seq.map (fun (_, n) -> n)
            |> Seq.sort
            |> Seq.toList

        match counts with
        | [2; 3] -> Some()
        | _ -> None

    match (category, dice) with
    | (Category.Yacht, IsYacht) -> 50
    | (Category.LittleStraight, IsLittleStraight) -> 30
    | (Category.BigStraight, IsBigStraight)  -> 30
    | (Category.Choice, _) -> Seq.sumBy dieScore dice
    | (Category.Ones,   NumberOfDies Die.One   count) -> count * 1
    | (Category.Twos,   NumberOfDies Die.Two   count) -> count * 2
    | (Category.Threes, NumberOfDies Die.Three count) -> count * 3
    | (Category.Fours,  NumberOfDies Die.Four  count) -> count * 4
    | (Category.Fives,  NumberOfDies Die.Five  count) -> count * 5
    | (Category.Sixes,  NumberOfDies Die.Six   count) -> count * 6
    | (Category.FourOfAKind, Has4OrMore (die, _)) -> (dieScore die) * 4
    | (Category.FullHouse, HasFullHouse) -> Seq.sumBy dieScore dice
    | _ -> 0
