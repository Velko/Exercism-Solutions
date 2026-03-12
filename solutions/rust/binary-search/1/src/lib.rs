pub fn find<A: AsRef<[T]>, T: PartialOrd>(collection: A, key: T) -> Option<usize> 
{
    let slice = collection.as_ref();

    let mut lo = 0;
    let mut hi = checked_dec(slice.len())?;

    while lo <= hi {
        let i = (lo + hi) / 2;
       
        if slice[i] < key {
            lo = i + 1;
        } else if slice[i] > key {
            hi = checked_dec(i)?;
        } else {
        	return Some(i);
        }
    }

    None
}

/* The typical algorithm simply uses signed integers for indexing. If 'hi' ever
   goes to -1, the loop terminates on it's condition, as 'lo' can never get 
   smaller than 0.
   
   Rust insists on using unsigned integer for indexing, but doing so causes
   "attempt to subtract with overflow" errors. Technically we could use signed,
   but that would require a lot of type-casting, which is not a good sign.

   To solve it more elegantly, we're introducing checked_dec() function that
   returns Some with result if it can be decremented and None if not. It then
   can be used with ? operator.

   Note: similar functionality is provided by CheckedSub trait in 'num' crate,
   but we're not going to import an extra crate for such a small function.
 */
fn checked_dec(value: usize) -> Option<usize> {
    if value > 0 {
        Some(value - 1)
    } else {
        None
    }
}