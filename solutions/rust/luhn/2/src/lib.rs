/// Check a Luhn checksum.
pub fn is_valid(code: &str) -> bool {

    if let Ok(digits) = parse_code(code) {

        let checksum: u32 = digits
            .iter()
            .rev()
            .enumerate()
            .map(|(i, d)|  if i % 2 == 1 {double_digit(*d)} else { *d })
            .sum();

        checksum % 10 == 0
    } else {
        false
    }
}

fn parse_code(code: &str) -> Result<Vec<u32>, ()> {
    let parsed_code: Result<Vec<_>, _> = code
        .chars()
        .map(parse_char)
        .collect();

    let digits: Vec<_> = parsed_code?
        .iter()
        .filter_map(|d| *d)
        .collect();

    if digits.len() > 1 {
        Ok(digits)
    } else {
        Err(())
    }
}

fn parse_char(c: char) -> Result<Option<u32>, ()> {
    match c {
        ' ' => Ok(None),
        _ if c.is_ascii_digit() => Ok(c.to_digit(10)),
        _ => Err(()),
    }
}

fn double_digit(d: u32) -> u32 {
    if d > 4 {
        d * 2 - 9
    } else {
        d * 2
    }
}
