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

    let mut cells = Vec::with_capacity(8);

    if y > 0 {
        if x > 0 {
            cells.push((x - 1, y - 1));
        }
        cells.push((x, y - 1));
        if x < width - 1 {
            cells.push((x + 1, y - 1));
        }
    }

    if x > 0 {
        cells.push((x - 1, y));
    }
    if x < width - 1 {
        cells.push((x + 1, y));
    }

    if y < height - 1 {
        if x > 0 {
            cells.push((x - 1, y + 1));
        }
        cells.push((x, y + 1));
        if x < width - 1 {
            cells.push((x + 1, y + 1));
        }
    }

    cells
}

fn to_cell_char(num: &u8) -> char{
    match num {
        b'0' => ' ',
        _ => *num as char
    }
}