use std::fs::File;
use std::io::BufReader;
use std::io::BufRead;
fn main() {
    let file = File::open("input.txt").unwrap();
    let mut reader = BufReader::new(file);
    let mut sums = vec![];
    let mut current_sum = 0; 
    for line in reader.lines(){
        let val = match line.expect("expectations").parse::<i32>() {
            Ok(num) => num,
            Err(_) => -1
        }; 
        if val > 0 {
            current_sum = current_sum + val; 
        } else {
            sums.push(current_sum);
            current_sum =0; 
        }
    }
    sums.sort();
    println!("Largest {:?}", sums);

}
