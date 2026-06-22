const OFFSETS: [(isize, isize); 8] = 
        [(-1, -1), (0, -1), (1, -1),
         (-1,  0),          (1,  0),
         (-1,  1), (0,  1), (1,  1)];

pub fn annotate(garden: &[&str]) -> Vec<String> {
    let mut result = Vec::with_capacity(garden.len());
    for (y, row) in garden.iter().enumerate() {
        let mut new_row = String::with_capacity(row.len());
        for (x, cell) in row.as_bytes().iter().enumerate() {
            if *cell == b' ' {
                let mut around = 0;
                for (dx, dy) in OFFSETS.into_iter() {
                    let tx = checked_add_with_bound(x, dx, row.len());
                    let ty = checked_add_with_bound(y, dy, garden.len());
                    
                    around += match get_neighbour(garden, tx, ty) {
                        Some(b'*') => 1,
                        _ => 0,
                    };
                }
                new_row.push(if around == 0 { ' '} else { (b'0' + around) as char});
            } else {
                new_row.push('*');    
            }
        }
        result.push(new_row)
    }
    
    result
}

fn get_neighbour(garden: &[&str], tx: Option<usize>, ty: Option<usize>) -> Option<u8> {
    Some(garden[ty?].as_bytes()[tx?])
}

fn checked_add_with_bound(base: usize, addend: isize, bound: usize) -> Option<usize> {
    let tentative = base.checked_add_signed(addend)?;
    
    if tentative < bound {
        Some(tentative)
    } else {
        None
    }
}