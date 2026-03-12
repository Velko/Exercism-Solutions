use std::cmp::Ordering;
use core::ops::Sub;

/// Given a list of poker hands, return a list of those hands which win.
///
/// Note the type signature: this function should return _the same_ reference to
/// the winning hand(s) as were passed in, not reconstructed strings which happen to be equal.

pub fn winning_hands<'a>(hands: &[&'a str]) -> Vec<&'a str> {

    let parsed_hands: Result<Vec<_>, ()> =
        hands
            .iter()
            .cloned()
            .map(Hand::from_str)
            .collect();

    // shadow original, assume it always parses fine
    let mut parsed_hands = parsed_hands.unwrap();

    // sort descending
    parsed_hands.sort_by(|a, b| b.cmp(a));

    let highest_hand = parsed_hands.first().unwrap();

    parsed_hands
        .iter()
        .take_while(|p| *p == highest_hand)
        .map(|o| o.original)
        .collect()
}


#[derive(Debug, PartialEq, Eq, PartialOrd, Ord)]
enum Combination {
    HighCard,
    Pair,
    TwoPair,
    ThreeOfAKind,
    Straight,
    Flush,
    FullHouse,
    FourOfAKind,
    StraightFlush,
    RoyalFlush,
}

#[derive(Debug, PartialEq, Eq, Clone, Copy)]
enum Suit {
    Clubs,
    Hearts,
    Diamonds,
    Spades,
}

// While first reaction is to create an enum, it complicates
// parsing and consecutiveness checks.
#[derive(Debug, PartialEq, Eq, Clone, Copy, PartialOrd, Ord)]
struct Rank(u8);

#[derive(Debug, Clone)]
struct Card {
    suit: Suit,
    rank: Rank,
}

#[derive(Debug, Eq)]
struct Hand<'a> {
    combination: Combination,
    significant_cards: Vec<Rank>,
    original: &'a str,
}

impl Suit {
    pub fn from_str(s: &str) -> Result<Self, ()> {
        match s {
            "C" => Ok(Self::Clubs),
            "H" => Ok(Self::Hearts),
            "D" => Ok(Self::Diamonds),
            "S" => Ok(Self::Spades),
            _ => Err(())
        }
    }
}

impl Rank {

    const A: Rank = Rank(14); // normal Ace
    const K: Rank = Rank(13);
    const Q: Rank = Rank(12);
    const J: Rank = Rank(11);
    // the numeric cards have value of 2 to 10
    const A_L: Rank = Rank(1); // Ace for low-straight checks

    pub fn from_str(s: &str) -> Result<Self, ()> {
        let num_res = s.parse::<u8>();

        match num_res {
            Ok(num) if (2..=10).contains(&num)  => Ok(Rank(num)),
            Err(_) =>
                match s {
                    "J" => Ok(Self::J),
                    "Q" => Ok(Self::Q),
                    "K" => Ok(Self::K),
                    "A" => Ok(Self::A),
                    _ => Err(())
                },
            Ok(_) => Err(())
        }
    }
}

impl Sub for Rank {
    type Output = isize;

    fn sub(self, rhs: Self) -> Self::Output {
        self.0 as isize - rhs.0 as isize
    }
}

impl Card {
    pub fn from_str(s: &str) -> Result<Self, ()> {
        Ok(Self {
            suit: Suit::from_str(&s[s.len()-1..])?,
            rank: Rank::from_str(&s[..s.len()-1])?,
        })
    }
}

impl Combination {

    pub fn detect(ranks: &[Rank], is_flush: bool) -> (Self, Vec<Rank>) {

        let straight = Self::check_straight(ranks);
        let grouped = Self::group_by_ranks(ranks);

        Self::try_royal_flush(straight, is_flush)
        .or(Self::try_straight_flush(straight, is_flush))
        .or(Self::try_n_of_a_kind(&grouped, 4, None, Self::FourOfAKind))
        .or(Self::try_n_of_a_kind(&grouped, 3, Some(2), Self::FullHouse))
        .or(Self::try_flush(is_flush, ranks))
        .or(Self::try_straight(straight))
        .or(Self::try_n_of_a_kind(&grouped, 3, None, Self::ThreeOfAKind))
        .or(Self::try_n_of_a_kind(&grouped, 2, Some(2), Self::TwoPair))
        .or(Self::try_n_of_a_kind(&grouped, 2, None, Self::Pair))
        .unwrap_or_else(|| Self::highest_card(ranks))
    }

    fn check_straight(cards: &[Rank]) -> Option<Rank> {
        if Self::are_consecutive(cards) {
            cards.first().copied()
        } else if cards.first() == Some(&Rank::A) {
            let mut low_straight = cards[1..].to_vec();
            low_straight.push(Rank::A_L);
            Self::check_straight(&low_straight)
        } else {
            None
        }
    }

    fn group_by_ranks(ranks: &[Rank]) -> Vec<(usize, Rank)> {
        let mut result = Vec::new();

        // cards are always ordered highest to lowest
        // rank, so we can loop and count them
        // It could probably be done using group_by from itertools crate or experimental feature slice_group_by,
        // but it's not that complicated anyway.
        let mut card_iter = ranks.iter().cloned();

        if let Some(card) = card_iter.next() {
            let mut current_rank = card;
            let mut count = 1;

            while let Some(card) = card_iter.next() {
                if card == current_rank {
                    count += 1;
                } else {
                    result.push((count, current_rank));
                    count = 1;
                    current_rank = card;
                }
            }

            result.push((count, current_rank));
        }

        result.sort_by(|a, b| b.cmp(a));

        result
    }

    fn are_consecutive(cards: &[Rank]) -> bool {
        cards
            .iter()
            .zip(cards
                .iter()
                .skip(1)
            )
            .all(|(a, b)| *a - *b == 1)
    }

    fn highest_card(ranks: &[Rank]) -> (Self, Vec<Rank>) {
        (Self::HighCard, ranks.to_vec())
    }

    fn try_n_of_a_kind(grouped: &[(usize, Rank)], first_group: usize, second_group: Option<usize>, combination: Self) -> Option<(Self, Vec<Rank>)> {
        match grouped {
            [(x, _), (y, _), ..] if *x == first_group && *y == second_group.unwrap_or(1) => {
                let ranks = grouped.iter().map(|(_, r)| *r).collect();
                Some((combination, ranks))
            },
            _ => None,
        }
    }

    fn try_straight(straight: Option<Rank>) -> Option<(Self, Vec<Rank>)> {
        Some((Self::Straight, vec![straight?]))
    }

    fn try_flush(is_flush: bool, ranks: &[Rank]) -> Option<(Self, Vec<Rank>)> {
        is_flush.then_some(())?;

        Some((Self::Flush, ranks.to_vec()))
    }

    fn try_straight_flush(straight: Option<Rank>, is_flush: bool) -> Option<(Self, Vec<Rank>)> {
        is_flush.then_some(())?;

        Some((Self::StraightFlush, vec![straight?]))
    }

    fn try_royal_flush(straight: Option<Rank>, is_flush: bool) -> Option<(Self, Vec<Rank>)> {
        (is_flush && straight == Some(Rank::A)).then_some(())?;

        Some((Self::RoyalFlush, Vec::new()))
    }
}

impl<'a> Hand<'a> {
    pub fn from_str(hand_str: &'a str) -> Result<Self, ()> {
        let hand: Result<Vec<_>, ()> = hand_str
            .split_whitespace()
            .map(Card::from_str)
            .collect();

        let hand = hand?;

        let mut ranks: Vec<_> = hand.iter().map(|c| c.rank).collect();
        ranks.sort_by(|a, b| b.cmp(a));

        let is_flush = Self::have_same_suits(&hand);

        let (combination, significant_cards) = Combination::detect(&ranks, is_flush);

        Ok(Self {
            combination,
            significant_cards,
            original: hand_str
        })
    }

    fn have_same_suits(cards: &[Card]) -> bool {
        let mut suits_iter = cards.iter().map(|c| c.suit);

        if let Some(suit) = suits_iter.next() {
            suits_iter.all(|s| s == suit)
        } else {
            false
        }
    }
}

impl PartialEq for Hand<'_> {
    fn eq(&self, other: &Self) -> bool {
        self.cmp(other).is_eq()
    }
}

impl PartialOrd for Hand<'_> {
    fn partial_cmp(&self, other: &Self) -> Option<Ordering> {
        Some(self.cmp(other))
    }
}

impl Ord for Hand<'_> {
    fn cmp(&self, other_hand: &Self) -> std::cmp::Ordering {
        match self.combination.cmp(&other_hand.combination) {
            Ordering::Greater => Ordering::Greater,
            Ordering::Less => Ordering::Less,
            Ordering::Equal => {
                self.significant_cards.cmp(&other_hand.significant_cards)
            },
        }
    }
}
