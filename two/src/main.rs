use std::fs::File;
use std::io::BufRead;
use std::io::BufReader;
#[derive(PartialEq)]
enum Rps{
    R = 1, 
    P = 2, 
    S = 3
}

fn main() {
    let file = File::open("input.txt").unwrap();
    let reader = BufReader::new(file);
    let mut score = 0;
    for line in reader.lines() {
        let mut split = line.as_ref().unwrap().split_whitespace();
        let first = split.next().unwrap();         
        let second = split.next().unwrap();
        let p1 = match first {
            "A" => Rps::R, 
            "B" => Rps::P, 
            "C" => Rps::S,
            &_ => todo!(),
        };
        let p2 = match second {
            "X" => Rps::R, 
            "Y" => Rps::P, 
            "Z" => Rps::S, 
            &_ => todo!(),
        };
        if p1 == Rps::R && p2 == Rps::P {
            score = score + 6;
        }
     }
    println!("Score {}", score);
}

// Info
// Rock A, X, 1
// Paper B, Y, 2
// Scissors C, Z, 3
// Win 6
// Draw 3
// Loss 0
