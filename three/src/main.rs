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
        let mut inter_char = 'a';
        second_half.chars().any(|c| {
            if first_half.contains(&c) {
                inter_char = c;
            }
            first_half.contains(&c)
        });
        let val = if inter_char.is_lowercase() { inter_char as i32 - 96} else { inter_char as i32 - 38 };
        println!("Cool {}, {}", inter_char, val);
        score = score + val; 
    }
    println!("Score {}", score);
}
