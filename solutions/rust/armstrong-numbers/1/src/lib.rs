pub fn is_armstrong_number(num: u32) -> bool {
    let num_str = num.to_string();


    let total = calc_total(&num_str);

    Ok(num) == total
}

fn calc_total(num_str: &str) -> Result<u32, ()> {
    let num_digits = num_str.len() as u32;

    num_str
        .chars()
        .map(|c| c
                .to_digit(10)
                .unwrap() // it's Ok to unwrap, because to_string() produces valid digits
                .checked_pow(num_digits)
                .ok_or(())
            )
        .try_fold(0u32, |acc, x| acc.checked_add(x?).ok_or(()))
}