pub fn find<A: AsRef<[T]>, T: PartialOrd>(collection: A, key: T) -> Option<usize> 
{
    let slice = collection.as_ref();

    let mut lo = 0;
    let mut hi = usize::checked_sub(slice.len(), 1)?;

    while lo <= hi {
        let i = (lo + hi) / 2;
       
        if slice[i] < key {
            lo = i + 1;
        } else if slice[i] > key {
            hi = usize::checked_sub(i, 1)?;
        } else {
        	return Some(i);
        }
    }

    None
}
