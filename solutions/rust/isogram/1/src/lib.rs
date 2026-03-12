use std::collections::HashSet;

pub fn check(candidate: &str) -> bool {
    let lower_alpha_only: Vec<_> = candidate
        .chars()
        .filter(char::is_ascii_alphabetic)
        .filter_map(|c|char::to_lowercase(c).next())
        .collect();
    
    let uniq_chars: HashSet::<_> =
        lower_alpha_only
        .iter()
        .collect();

    uniq_chars.len() == lower_alpha_only.len()
}