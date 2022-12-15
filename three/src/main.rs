#![feature(iter_array_chunks)]
use std::collections::HashSet;
use std::fs::File;
use std::io::BufRead;
use std::io::BufReader;

fn main() {
    let file = File::open("input.txt").unwrap();
    let reader = BufReader::new(file);
    let mut score = 0;
    for [first, second, third] in reader.lines().array_chunks() { 
        let first_hash: HashSet<char> = first.unwrap().chars().collect();
        let second_hash: HashSet<char> = second.unwrap().chars().collect();
        let third_hash: HashSet<char> = third.unwrap().chars().collect();
        let first_inter:HashSet<char> = first_hash.intersection(&second_hash).cloned().collect();
        let second_inter:HashSet<char> = first_inter.intersection(&third_hash).cloned().collect();
        let cool = second_inter.iter().next().unwrap().clone();
        let val = if cool.is_lowercase() {cool as i32 - 96} else {cool as i32 -38};
        score = score + val; 
    }
    println!("Score {}", score);
}


//let val = if c.is_lowercase() {
//                    c as i32 - 96
 //               } else {
  //                  c as i32 - 38
   //             };
    //            score = score + val;

