/// Check a Luhn checksum.
pub fn is_valid(code: &str) -> bool {
    let parsed_code: Vec<_> = code
        .chars()
        .filter_map(parse_char)
        .collect();

    if parsed_code.iter().any(Result::is_err) { return false;}

    let digits: Vec<_>= parsed_code.iter().map(|d| d.unwrap())
        .collect();

    if digits.len() < 2 { return false; }

    let checksum: u16 = digits
        .iter()
        .rev()
        .enumerate()
        .map(|(i, d)|  if i % 2 == 1 {double_digit(*d)} else { *d })
        .sum();

    checksum % 10 == 0
}


fn parse_char(c: char) -> Option<Result<u16, ()>> {
    match c {
        ' ' => None,
        _ if c.is_ascii_digit() => Some(Ok((c as u8 - b'0') as u16)),
        _ => Some(Err(())),
    }
}

fn double_digit(d: u16) -> u16 {
    let res = d * 2;

    if res > 9 {
        res - 9
    } else {
        res
    }
}
