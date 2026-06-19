#include "sieve.h"
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>

#define FIRST_PRIME    2   /* Teal'c 😉 */

uint32_t sieve(uint32_t limit, uint32_t *primes, size_t max_primes) {
    // allocate and fill an array for up to *limit* (inclusive)
    bool *prime_map = malloc(sizeof(bool) * (limit + 1));
    memset(prime_map, true, sizeof(bool) * (limit + 1));

    // 0 and 1 are not primes (these elements are never touched by
    // the loops, but still better set them explicitly)
    prime_map[0] = prime_map[1] = false;

    for (size_t prime = FIRST_PRIME; prime <= limit; ++prime) {
        if (!prime_map[prime]) continue;
        for (size_t multiple = prime * prime; multiple <= limit; multiple += prime)
            prime_map[multiple] = false;
    }

    size_t nprimes = 0;
    for (size_t prime = FIRST_PRIME; prime <= limit && nprimes < max_primes; ++prime) {
        if (prime_map[prime])
            primes[nprimes++] = prime;
    }
    
    free(prime_map);
    return nprimes;
}