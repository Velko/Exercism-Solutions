// Incremental sieve
// Based on: https://www.cs.hmc.edu/~oneill/papers/Sieve-JFP.pdf

use std::cmp::Ordering;
use std::collections::BinaryHeap;

/* Instead of allocating a big array and then "crossing off" prime composites,
   this approach does it virtually, by keeping a sorted list of known upcoming
   composites and skipping over those.

   Each found prime creates a new "composite item" on the list. When non-prime
   is detected (the list had it), item is removed, new item is calculated and
   pushed back to the list. */


#[derive(Copy, Clone, Eq, PartialEq)]
struct PrimeComposite {
    current_composite: u64,
    base_prime: u64,
}

impl PrimeComposite {
    pub fn new(prime: u64) -> Self {
        /* Prime composite for the first time becomes relevant at square
           of the base prime. Up until that it's composites are "covered"
           by smaller numbers */
        Self {
            current_composite: prime * prime,
            base_prime: prime
        }
    }

    fn next(&self) -> Self {
        /* Further it has to be considered every time */
        Self {
            current_composite: self.current_composite + self.base_prime, 
            base_prime: self.base_prime
        }
    }
}

/* BinaryHeap requires it's items to implement Ord trait. We need to keep them
   in ascending order by current_composite value. BinaryHeap is a max-heap, in 
   order for it to behave as min-heap, the arguments in cmp() are swapped.
*/
impl Ord for PrimeComposite {
    fn cmp(&self, other: &Self) -> Ordering {
        other.current_composite.cmp(&self.current_composite)
    }
}

impl PartialOrd for PrimeComposite {
    fn partial_cmp(&self, other: &Self) -> Option<Ordering> {
        other.current_composite.partial_cmp(&self.current_composite)
    }
}


/* Implementing The Sieve as an end-less iterator, returning primes */
struct PrimesIterator {
    composites_queue: BinaryHeap<PrimeComposite>,
    candidate: u64,
}

impl PrimesIterator {
    pub fn new() -> Self {
        Self {
            composites_queue: BinaryHeap::new(),
            candidate: 1, // initialize to 1, so the next number to consider is 2
        }
    }

    /* Check if the top item is equal to current candidate and pop it off. Function is
       called repeatedly, until all items for the candidate are consumed. */
    fn pop_composite(&mut self, candidate: u64) -> Option<PrimeComposite> {
        match self.composites_queue.peek() {
            Some(item) if item.current_composite == candidate => self.composites_queue.pop(),
            _ => None
        }
    }
}

impl Iterator for PrimesIterator {
    type Item = u64;

    fn next(&mut self) -> Option<Self::Item> {

        loop {
            // next candidate to consider
            self.candidate += 1;
            let mut is_prime = true;

            // check if there are composites items on the list for it,
            // adjust and re-add items for future candidates
            while let Some(cross) = self.pop_composite(self.candidate) {
                self.composites_queue.push(cross.next());
                is_prime = false;
            }

            // there were no items on the list - it was a prime
            if is_prime { break; }
        }

        // each prime detected adds a new item on the list
        self.composites_queue.push(PrimeComposite::new(self.candidate));

        // yay, a prime to return from iterator
        Some(self.candidate)
    }
}


pub fn primes_up_to(upper_bound: u64) -> Vec<u64> {

    // With all the hocus-pocus above, the main function becomes trivial.

    PrimesIterator::new()
        .take_while(|p| p <= &upper_bound)
        .collect()
    
}
