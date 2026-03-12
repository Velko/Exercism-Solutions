use itertools::Itertools;

#[derive(Debug)]
pub enum Category {
    Ones,
    Twos,
    Threes,
    Fours,
    Fives,
    Sixes,
    FullHouse,
    FourOfAKind,
    LittleStraight,
    BigStraight,
    Choice,
    Yacht,
}

type Dice = [u8; 5];

pub fn score(dice: Dice, category: Category) -> u8 {
    match category {
        Category::Yacht if IsYacht(&dice) => 50,
        Category::Ones => TotalOf(&dice, 1),
        Category::Twos => TotalOf(&dice, 2),
        Category::Threes => TotalOf(&dice, 3),
        Category::Fours => TotalOf(&dice, 4),
        Category::Fives => TotalOf(&dice, 5),
        Category::Sixes => TotalOf(&dice, 6),
        Category::FourOfAKind => if let Some(die) = Has4OrMore(&dice) { die * 4 } else { 0 },
        Category::LittleStraight if IsLittleStraight(&dice) => 30,
        Category::BigStraight if IsBigStraight(&dice) => 30,
        Category::Choice => dice.into_iter().sum(),
        _ => 0,
    }
}


fn IsYacht(dice: &[u8]) -> bool {
    let mut iter = dice.into_iter();

    if let Some(first) = iter.next() {
        iter.all(|elem| elem == first)
    } else {
        false
    }
}

fn TotalOf(dice: &[u8], value: u8) -> u8 {
    dice
        .into_iter()
        .filter (|d| **d == value)
        .sum()
}

fn Has4OrMore(dice: &[u8]) -> Option<u8> {
    dice
        .into_iter()
        .sorted()
        .group_by(|d| **d)
        .into_iter()
        .map(|(d, g)| (d, g.count()))
        .filter(|(_, g)| *g >= 4)
        .map(|(d, _)| d)
        .next()
}

fn IsLittleStraight(dice: &[u8]) -> bool {
    let sortedDice: Vec<u8> = dice
        .into_iter()
        .cloned()
        .sorted()
        .collect();

    vec![1, 2, 3, 4, 5] == sortedDice
}

fn IsBigStraight(dice: &[u8]) -> bool {
    let sortedDice: Vec<u8> = dice
        .into_iter()
        .cloned()
        .sorted()
        .collect();

    vec![2, 3, 4, 5, 6] == sortedDice
}
