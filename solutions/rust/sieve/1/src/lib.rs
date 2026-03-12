// Basic Simple sieve implementation

pub fn primes_up_to(upper_bound: u64) -> Vec<u64> {

    let mut primes = Vec::new();
    if upper_bound < 2 {
        return primes;
    }  

    let upper_limit = upper_bound as usize + 1;
   
    let mut candidates = vec![true; upper_limit];
    candidates[0] = false;
    candidates[1] = false;

    for p in 2..upper_limit {
        if !candidates[p] {
            continue;
        }

        primes.push(p as u64);

        for m in (p+p..upper_limit).step_by(p) {
            candidates[m] = false;
        }
    }

    primes
}
