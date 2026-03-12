use std::collections::HashSet;

pub fn check(candidate: &str) -> bool {
    let lower_alpha_only: Vec<_> = candidate
        .chars()
        .filter(char::is_ascii_alphabetic)
        .map(|c|char::to_ascii_lowercase(&c))
        .collect();
    
    let uniq_chars: HashSet::<_> =
        lower_alpha_only
        .iter()
        .collect();

    uniq_chars.len() == lower_alpha_only.len()
}