pub fn primes(buffer: []u32, limit: u32) []u32 {
    var sieve = [_]bool{true} ** 1001;
    sieve[0] = false;
    sieve[1] = false;
    var count: u32 = 0;
    for (sieve[0..limit+1], 0..) |is_prime, p| {
        if (is_prime) {
            buffer[count] = @intCast(p);
            count += 1;

            var m = p * p;
            while (m <= limit) {
                sieve[m] = false;
                m += p;
            }
        }
    }
    return buffer[0..count];
}
