pub fn verse(n: u32) -> String {
    capitalize(
        format!("{}, {}.\n{}\n",
            format_wall(n), bottles_of_beer(n), format_remaining(n, 99)
        )
    )
}

fn format_number(n: u32) -> String {
    if n == 0 {
        "no more".to_string()
    } else {
        n.to_string()
    }
}

fn bottles(n: u32) -> String {
    format!("bottle{}", if n == 1 {
            ""
        } else {
            "s"
        }
    )
}

fn bottles_of_beer(n: u32) -> String {
    format!("{} {} of beer", format_number(n), bottles(n))
}

fn capitalize(s: String) -> String {
    s[0..1].to_uppercase() + &s[1..]
}

fn format_single(n: u32) -> &'static str {
    if n == 1 {
        "it"
    } else {
        "one"
    }
}

fn format_wall(n: u32) -> String {
    format!("{0} on the wall", bottles_of_beer(n))
}

fn format_remaining(n: u32, reset: u32) -> String {
    if n == 0 {
        format!("Go to the store and buy some more, {}.", format_wall(reset))
    } else {
        format!("Take {} down and pass it around, {}.", format_single(n), format_wall(n - 1))
    }
}

pub fn sing(start: u32, end: u32) -> String {
    (end..=start).rev().map(verse).collect::<Vec<_>>().join("\n")
}
