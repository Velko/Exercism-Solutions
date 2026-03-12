package sieve

import "fmt"

const SEG_SIZE = 32
const RANGE_MAX = SEG_SIZE * SEG_SIZE

func Sieve(limit int) []int {

    // the segmented sieve implementation is limited up to the square of segment size
	if limit >= RANGE_MAX {
    	panic(fmt.Sprintf("Only limit < %d is supported", RANGE_MAX))
    }

    // the resulting list of primes
	var primes []int

    // seg0 serves dual purpose:
    // * non-zero value at array element indicates it's index is a prime
    // * the value stored is largest multiple of the prime seen yet
    var seg0 [SEG_SIZE]int

    // 0-th segment is calculated using Simple Sieve algorithm
    
    // initialize all elements to non-zero, except for 0-th and 1-st
    for i := range seg0 { seg0[i] = -1 }
    seg0[0] = 0; seg0[1] = 0

    // strike-out multiples of primes
    for prime := range seg0 {
        if seg0[prime] == 0 { continue }

        // put prime on result list
        if prime > limit { break }
        primes = append(primes, prime)

        var multiple int
        for multiple = prime + prime; multiple < SEG_SIZE; multiple += prime {
            seg0[multiple] = 0
        }

        seg0[prime] = multiple // store highest multiple for next segment
    }

    // proceed with Segmented Sieve - calculate n-th segments
    for seg_start := SEG_SIZE; seg_start < limit; seg_start += SEG_SIZE {
        var seg_n [SEG_SIZE]bool

        // initialize to all-true
        for i := range seg_n { seg_n[i] = true }

        // apply primes from seg0 to strike-out multiples in the segment
        for prime, multiple := range seg0 {
            if multiple == 0 { continue }

            // index of the multiple in current segment
            m_idx := multiple - seg_start

            for ; m_idx < SEG_SIZE; m_idx += prime {
                seg_n[m_idx] = false
            }

            // store largest seen multiple
            seg0[prime] = m_idx + seg_start
        }

        // put found primes on result list
        for i, val := range seg_n {
            if val {
                var prime = i + seg_start
                if prime > limit { break }
        		primes = append(primes, prime)
            }
        }
    }
    
    return primes
}
