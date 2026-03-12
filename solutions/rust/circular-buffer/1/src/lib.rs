use std::mem::MaybeUninit;
use std::{iter, mem};

pub struct CircularBuffer<T> {
    buffer: Box<[MaybeUninit<T>]>,
    r_idx: usize,
    w_idx: usize,
}

#[derive(Debug, PartialEq, Eq)]
pub enum Error {
    EmptyBuffer,
    FullBuffer,
}

/* On disposal, CircularBuffer may contain items, that needs to be disposed
   properly. Implementing Drop trait for that. */
impl<T> Drop for CircularBuffer<T> {
    fn drop(&mut self) {
        self.clear();
    }
}

impl<T> CircularBuffer<T> {
    pub fn new(capacity: usize) -> Self {
        /* Create a vector of "uninitialized" values and convert into boxed
           slice of required length. */
        let buf: Vec<_> = iter::repeat_with(MaybeUninit::uninit)
            .take(capacity)
            .collect();
        Self {
            buffer: buf.into_boxed_slice(),
            r_idx: 0,
            w_idx: 0,
        }
    }

    pub fn write(&mut self, element: T) -> Result<(), Error> {
        if self.is_full() {
            Err(Error::FullBuffer)
        } else {
            self.buffer[self.wrapped_w_idx()] = MaybeUninit::new(element);
            self.w_idx = self.increment(self.w_idx);
            Ok(())
        }
    }

    pub fn read(&mut self) -> Result<T, Error> {
        if self.is_empty() {
            Err(Error::EmptyBuffer)
        } else {
            /* Extract the item and replace the slot with an "uninitialized" value */
            let item = mem::replace(&mut self.buffer[self.wrapped_r_idx()], MaybeUninit::uninit());
            self.r_idx = self.increment(self.r_idx);
            Ok(
                /* item was initialized by MaybeUninit::new() when it was inserted */
                unsafe {
                    item.assume_init()
                }
            )
        }
    }

    pub fn clear(&mut self) {
        /* Quick and wrong way would be to simply manipulate read/write indices. We need to
           dispose the items properly. */
        while !self.is_empty() {
            _ = self.read();
        }
    }

    pub fn overwrite(&mut self, element: T) {
        if self.is_full() {
            /* Discard an item, making room for a new one */
            _ = self.read();
        }

        /* Write should not fail, as we just made a room */
        self.write(element).unwrap();
    }


    /* r_idx and w_idx increments are wrapped around at twice the capacity of the buffer. "Cheaper"
       implementation could simply increment them and hope that they never reach usize's overflow.

       To access items in buffer, indices still need to be wrapped to the capacity of the buffer.

       CircularBuffer's empty condition is when both indices are equal. Full condition is when distance
       between the indices is equal to the capacity. Since they wrap around, this translates to their
       capacity-wrapped versions pointing to same item, but full versions being different.
     */

    pub fn is_empty(&self) -> bool {
        self.r_idx == self.w_idx
    }

    pub fn is_full(&self) -> bool {
        self.r_idx != self.w_idx && self.wrapped_r_idx() == self.wrapped_w_idx()
    }

    fn wrapped_r_idx(&self) -> usize {
        self.r_idx % self.buffer.len()
    }

    fn wrapped_w_idx(&self) -> usize {
        self.w_idx % self.buffer.len()
    }

    fn increment(&self, idx: usize) -> usize {
        (idx + 1) % (self.buffer.len() * 2)
    }
}
