use std::{collections::HashSet};

pub fn anagrams_for<'a>(word: &str, possible_anagrams: &[&'a str]) -> HashSet<&'a str> {
    let sorted_word = sorted_str(word);

    let desas = possible_anagrams.iter().cloned()
        .filter(|anagram| 
            word.to_lowercase() != anagram.to_lowercase()
            && sorted_word == sorted_str(anagram));

    HashSet::from_iter(desas)
}


fn sorted_str(word: &str) -> Vec<char> {
    let mut s: Vec<char> = word.to_lowercase().chars().collect();
    s.sort_unstable();
    s
}