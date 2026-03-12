use std::collections::HashMap;
use std::collections::hash_map::Entry;

pub fn can_construct_note(magazine: &[&str], note: &[&str]) -> bool {
    let mut words_in_magazine: HashMap<&str, usize> = HashMap::new();

    for word in magazine {
        *words_in_magazine.entry(word).or_default() += 1;
    }

    for word in note {
        match words_in_magazine.entry(word) {
            Entry::Occupied(mut entry) if *entry.get() > 0 => *entry.get_mut() -=1,
            _ => return false,
        }
    }

    true
}
