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
            "X" => "lose", 
            "Y" => "draw", 
            "Z" => "win", 
            &_ => todo!(),
        }; 
        let cool = match (p1, p2) {
            (Rps::R, "lose") => 3 + 0, 
            (Rps::R, "draw") => 1 + 3, 
            (Rps::R, "win") =>  2 + 6,
            (Rps::P, "lose") => 1 + 0, 
            (Rps::P, "draw") => 2 + 3,
            (Rps::P, "win") => 3 + 6, 
            (Rps::S, "lose") => 2 + 0,
            (Rps::S, "draw") => 3 + 3, 
            (Rps::S, "win") => 1 + 6,
            (Rps::R, &_) | (Rps::P, &_) | (Rps::S, &_) => todo!()
        };
        score = score + cool;
     }
    println!("Score {}", score);
}

// Info
// Win 6
// Draw 3
// Loss 0
