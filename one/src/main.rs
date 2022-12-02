use std::fs::File;
use std::io::BufRead;
use std::io::BufReader;
fn main() {
    let file = File::open("input.txt").unwrap();
    let reader = BufReader::new(file);
    let mut sums = vec![];
    let mut current_sum = 0;
    for line in reader.lines() {
        match line.expect("expectations").parse::<i32>() {
            Ok(num) => {
                current_sum = current_sum + num;
            }
            Err(_) => {
                sums.push(current_sum);
                current_sum = 0;
            }
        };
    }
    sums.sort();
    println!("Largest {:?}", sums);
}
