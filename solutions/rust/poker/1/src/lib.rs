use std::cmp::Ordering;
use core::ops::Sub;

/// Given a list of poker hands, return a list of those hands which win.
///
/// Note the type signature: this function should return _the same_ reference to
/// the winning hand(s) as were passed in, not reconstructed strings which happen to be equal.

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
        let straight = Self::check_straight(&ranks);
        let grouped = Self::group_by_ranks(&ranks);

        let (combination, significant_cards) =
             Self::try_royal_flush(straight, is_flush)
            .or(Self::try_straight_flush(straight, is_flush))
            .or(Self::try_four_of_a_kind(&grouped))
            .or(Self::try_full_house(&grouped))
            .or(Self::try_flush(is_flush, &ranks))
            .or(Self::try_straight(straight))
            .or(Self::try_three_of_a_kind(&grouped))
            .or(Self::try_two_pair(&grouped))
            .or(Self::try_pair(&grouped))
            .or(Self::try_highest_card(&ranks))
            .unwrap();

        Ok(Self {
            combination,
            significant_cards,
            original: hand_str
        })
    }

    fn check_straight(cards: &[Rank]) -> Option<Rank> {
        if Self::are_consecutive(cards) {
            Some(cards[0])
        } else if cards[0] == Rank::A {
            let mut low_straight = cards[1..].to_vec();
            low_straight.push(Rank::A_L);
            if Self::are_consecutive(&low_straight) {
                Some(low_straight[0])
            } else {
                None
            }
        } else {
            None
        }
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

    fn have_same_suits(cards: &[Card]) -> bool {
        let mut suits_iter = cards.iter().map(|c| c.suit);

        if let Some(suit) = suits_iter.next() {
            suits_iter.all(|s| s == suit)
        } else {
            false
        }
    }

    fn group_by_ranks(ranks: &[Rank]) -> Vec<(usize, Rank)> {
        let mut result = Vec::new();

        // cards are always ordered highest to lowest
        // rank, so we can loop and count them
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

    fn try_highest_card(ranks: &[Rank]) -> Option<(Combination, Vec<Rank>)> {
        Some((Combination::HighCard, ranks.to_vec()))
    }

    fn try_pair(grouped: &[(usize, Rank)]) -> Option<(Combination, Vec<Rank>)> {
        if let [(2, _), ..] = grouped {
            let ranks = grouped.iter().map(|(_, r)| *r).collect();
            Some((Combination::Pair, ranks))
        } else {
            None
        }
    }

    fn try_two_pair(grouped: &[(usize, Rank)]) -> Option<(Combination, Vec<Rank>)> {
        if let [(2, _), (2, _), ..] = grouped {
            let ranks = grouped.iter().map(|(_, r)| *r).collect();
            Some((Combination::TwoPair, ranks))
        } else {
            None
        }
    }

    fn try_three_of_a_kind(grouped: &[(usize, Rank)]) -> Option<(Combination, Vec<Rank>)> {
        if let [(3, _), ..] = grouped {
            let ranks = grouped.iter().map(|(_, r)| *r).collect();
            Some((Combination::ThreeOfAKind, ranks))
        } else {
            None
        }
    }

    fn try_straight(straight: Option<Rank>) -> Option<(Combination, Vec<Rank>)> {
        Some((Combination::Straight, vec![ straight?]))
    }

    fn try_flush(is_flush: bool, ranks: &[Rank]) -> Option<(Combination, Vec<Rank>)> {
        if is_flush {
            Some((Combination::Flush, ranks.to_vec()))
        } else {
            None
        }
    }

    fn try_full_house(grouped: &[(usize, Rank)]) ->  Option<(Combination, Vec<Rank>)> {
        if let [(3, _), (2, _)] = grouped {
            let ranks = grouped.iter().map(|(_, r)| *r).collect();
            Some((Combination::FullHouse, ranks))
        } else {
            None
        }
    }

    fn try_four_of_a_kind(grouped: &[(usize, Rank)]) ->  Option<(Combination, Vec<Rank>)> {
        if let [(4, _), (1, _)] = grouped {
            let ranks = grouped.iter().map(|(_, r)| *r).collect();
            Some((Combination::FourOfAKind, ranks))
        } else {
            None
        }
    }

    fn try_straight_flush(straight: Option<Rank>, is_flush: bool) -> Option<(Combination, Vec<Rank>)> {
        if is_flush {
            Some((Combination::StraightFlush, vec![straight?]))
        } else {
            None
        }
    }

    fn try_royal_flush(straight: Option<Rank>, is_flush: bool) -> Option<(Combination, Vec<Rank>)> {
        if is_flush {
            if straight == Some(Rank::A) {
                Some((Combination::RoyalFlush, Vec::new()))
            } else {
                None
            }
        } else {
            None
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
