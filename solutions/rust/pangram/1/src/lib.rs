/// Determine whether a sentence is a pangram.
pub fn is_pangram(sentence: &str) -> bool {
    let mut bitmask: u32 = 0;
    for c in sentence.chars() {
        match char_index_in_alphabet(c) {
            Some(i) => bitmask |= 1 << i,
            None => {},
        }
    }
    bitmask == ALL_BITS_IN_ALPHABET
}

fn char_index_in_alphabet (c: char) -> Option<u8> {
    match c {
        'a'..='z' => Some(c as u8 - 'a' as u8),
        'A'..='Z' => Some(c as u8 - 'A' as u8),
        _ => None
    }
}

const ALL_BITS_IN_ALPHABET: u32 = (1 << 26) - 1;