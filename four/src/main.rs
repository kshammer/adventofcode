use std::fs::File;
use std::io::BufRead;
use std::io::BufReader;

fn main() {
    let file = File::open("input.txt").unwrap();
    let reader = BufReader::new(file);
    let mut score = 0;
    for line in reader.lines() {
        let cool_line = line.unwrap(); // setting value for simplier borrow checker
        let mut split = cool_line.split(",");
        let first_range: Vec<i32> = split
            .next()
            .unwrap()
            .split("-")
            .map(|word| word.parse().unwrap())
            .collect();
        let second_range: Vec<i32> = split
            .next()
            .unwrap()
            .split("-")
            .map(|word| word.parse().unwrap())
            .collect();
        println!("First {:?}, second {:?}", first_range, second_range);
        if (first_range[0] <= second_range[0] && second_range[0] <= first_range[1])
            || (second_range[0] <= first_range[0] && first_range[0] <= second_range[1])
        {
            score = score + 1;
        }
    }
    println!("Score {}", score);
}
