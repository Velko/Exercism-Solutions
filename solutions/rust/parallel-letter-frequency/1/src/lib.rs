use std::collections::HashMap;
use std::mem::transmute;
use std::thread;

pub fn frequency(input: &[&str], worker_count: usize) -> HashMap<char, usize> {

    // threads are joined back within this function, while references are still
    // valid, so it is safe to "grant" a larger lifetime
    let input = unsafe { transmute::<&[&str], &'static [&str]>(input) };

    let mut workers = Vec::with_capacity(worker_count);
    for chunk in input.chunks(input.len() / worker_count + 1) {

        let worker = thread::spawn(move || {
            count_chars(chunk)
        });

        workers.push(worker);
    }

    let mut freq: HashMap<char, usize> = HashMap::new();
    for worker in workers {
        let result = worker.join().unwrap(); // if thread fails something went really wrong. Will not try to recover.
        for (c, n) in result.into_iter() {
            *freq.entry(c).or_default() += n;
        }
    }

    freq
}

fn count_chars(input: &[&str]) -> HashMap<char, usize> {
    let mut freq: HashMap<char, usize> = HashMap::new();
    for line in input {
        for chr in line.chars().filter(|c| c.is_alphabetic()) {
            *freq.entry(chr.to_ascii_lowercase()).or_default() += 1;
        }
    }

    freq
}