use std::collections::HashSet;
use std::fs::File;
use std::io::BufRead;
use std::io::BufReader;

fn main() {
    let file = File::open("input.txt").unwrap();
    let reader = BufReader::new(file);
    let mut score = 0;
    for line in reader.lines() {
        let cool_str = line.unwrap();
        let first_half: HashSet<char> = cool_str.chars().take(cool_str.len() / 2).collect();
        let second_half: String = cool_str
            .chars()
            .skip(cool_str.len() / 2)
            .take(cool_str.len())
            .collect();
        second_half.chars().any(|c| {
            if first_half.contains(&c) {
                let val = if c.is_lowercase() {
                    c as i32 - 96
                } else {
                    c as i32 - 38
                };
                score = score + val;
            }
            first_half.contains(&c)
        });
    }
    println!("Score {}", score);
}
