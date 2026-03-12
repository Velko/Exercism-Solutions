use std::cmp::Ordering;

pub fn find<A: AsRef<[T]>, T: PartialOrd>(collection: A, key: T) -> Option<usize>
{
    let slice = collection.as_ref();

    let mut lo = 0;
    let mut hi = slice.len();

    while lo < hi {
        let i = (lo + hi) / 2;

        match key.partial_cmp(&slice[i])? {
            Ordering::Greater => lo = i + 1,
            Ordering::Less => hi = i,
            Ordering::Equal => return Some(i),
        }
    }

    None
}
