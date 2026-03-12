pub fn annotate(minefield: &[&str]) -> Vec<String> {
    let height = minefield.len();
    if height < 1 { return Vec::new() }

    let width = minefield[0].len();
    let mut field = vec![vec![b'0'; width]; height];

    for (y, row) in minefield.iter().enumerate() {
        for (x, cell) in row.as_bytes().iter().enumerate() {
            if *cell == b'*' {
                field[y][x] = *cell;
                for (a_x, a_y) in cells_around(x, y, width, height) {
                    if field[a_y][a_x] != b'*' {
                        field[a_y][a_x] += 1;
                    }
                }
            }
        }
    }

    field
        .iter()
        .map(|row| row
                    .iter()
                    .map(to_cell_char)
                    .collect())
        .collect()
}

fn cells_around(x: usize, y: usize, width: usize, height: usize) -> Vec<(usize, usize)> {

    let neighbours = vec![(-1, -1), (0, -1), (1, -1),
                          (-1,  0),          (1,  0),
                          (-1,  1), (0,  1), (1,  1)];

    neighbours.iter()
        .filter_map(|(c_x, c_y)| Some((checked_add_limited(x, *c_x, width)?, checked_add_limited(y, *c_y, height)?)))
                .collect()
}

fn checked_add_limited(value: usize, offset: isize, upper_limit: usize) -> Option<usize> {
    let result = value.checked_add_signed(offset)?;

    if result < upper_limit {
        Some(result)
    }
    else {
        None
    }
}

fn to_cell_char(num: &u8) -> char{
    match num {
        b'0' => ' ',
        _ => *num as char
    }
}