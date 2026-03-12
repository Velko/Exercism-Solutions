package sieve

import "fmt"

const SEG_SIZE = 32
const RANGE_MAX = SEG_SIZE * SEG_SIZE

func Sieve(limit int) []int {

    // the segmented sieve implementation is limited up to the square of segment size
	if limit >= RANGE_MAX {
    	panic(fmt.Sprintf("Only limit < %d is supported", RANGE_MAX))
    }
    
	var primes []int
    
    var seg0 [SEG_SIZE]int

    // simple sieve for 0-th segment
    seg0[0] = 0; seg0[1] = 0
    for p := 2; p < SEG_SIZE; p++ { seg0[p] = -1 }

    for p := 2; p < SEG_SIZE; p++ {
        if seg0[p] == 0 { continue }

        if p > limit { break }
        primes = append(primes, p)

        var m int
        for m = p + p; m < SEG_SIZE; m += p {
            seg0[m] = 0
        }

        seg0[p] = m // store highest multiple for next segment
    }

    // proceed to calculate n-th segments
    for r_low := SEG_SIZE; r_low < limit; r_low += SEG_SIZE {
        var seg_n [SEG_SIZE]bool

        for p := 0; p < SEG_SIZE; p++ { seg_n[p] = true }

        for p := 2; p < SEG_SIZE; p++ {
            if seg0[p] == 0 { continue }

            m := seg0[p] - r_low

            for ; m < SEG_SIZE; m += p {
                seg_n[m] = false
            }

            seg0[p] = m + r_low
        }

        for i := 0; i < SEG_SIZE; i++ {
            if seg_n[i] {
                var p = i + r_low
                if p > limit { break }
        		primes = append(primes, p)
            }
        }
    }
    
    return primes
}
