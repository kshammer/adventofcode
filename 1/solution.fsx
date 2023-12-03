open System

// Solution 1
let createInt (numList: int list) =
    (numList.Head * 10) + (List.rev numList).Head

let selectNumbers (elem: char) =
    match elem with
    | elem when Char.IsNumber elem -> Some(int (Char.GetNumericValue elem))
    | _ -> None

let convertStringToCalibrationValue (input: string) =
    Seq.toList input |> List.choose selectNumbers |> createInt

let solution1 input =
    List.map convertStringToCalibrationValue input
    |> List.fold (fun acc elem -> acc + elem) 0

let testInput = [ "1abc2"; "pqr3stu8vwx"; "a1b2c3d4e5f"; "treb7uchet" ]

let testCalibrationSum = solution1 testInput

// printfn "test values first solution %O" testCalibrationSum

let actualInput = System.IO.File.ReadLines "input.txt" |> List.ofSeq

let calibrationSum = solution1 actualInput

// printfn "first puzzle %O" calibrationSum

// Solution 2

let convertStringToNumber (input: string) = 
    input
        .Replace("eightwo", "82")
        .Replace("eighthree", "83")
        .Replace("twone", "21")
        .Replace("nineight", "98")
        .Replace("oneight", "18")
        .Replace("fiveight", "58")
        .Replace("threeight", "38")
        .Replace("one", "1")
        .Replace("two", "2")
        .Replace("three", "3")
        .Replace("four", "4")
        .Replace("five", "5")
        .Replace("six", "6")
        .Replace("seven", "7")
        .Replace("eight", "8")
        .Replace("nine", "9")

let testInputPuzzleTwo = ["two1nine"; "eightwothree"; "abcone2threexyz"; "xtwone3four"; "4nineeightseven2"; "zoneight234"; "7pqrstsixteen"]

let solution2 input = List.map convertStringToNumber input 

let cool = solution2 testInputPuzzleTwo |> solution1

printfn "test input problem2 %A" cool

let nice = solution2 actualInput |> solution1

printfn "real input problem2 %O" nice