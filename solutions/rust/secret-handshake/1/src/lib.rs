const SIGNS:  &'static [&'static str] = &["wink", "double blink", "close your eyes", "jump"];
const REVERSE_MASK: u8 = 0b10000;

pub fn actions(n: u8) -> Vec<&'static str> {
    
    SIGNS.iter()
        .enumerate()
        .filter(move |(idx, _)| n & (1 << idx) != 0 )
        .map (|(_ , sign)| *sign)
        .cond_rev(n & REVERSE_MASK != 0)
        .collect()
}

trait CondRev: Iterator
    where
        Self: DoubleEndedIterator,
        Self: Sized,
        Self: 'static
    {
        fn cond_rev(self, reverse: bool) -> Box<dyn Iterator<Item=Self::Item>>
        {
            if reverse {
                Box::new(self.rev())
            } else {
                Box::new(self)
            }
        }
}

impl<I: DoubleEndedIterator + 'static> CondRev for I {}

