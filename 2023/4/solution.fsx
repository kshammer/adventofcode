open System
open System.Text.RegularExpressions

let sampleInput = "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53"
let edgeCase = "Card 1: 1 1 1 1  | 1"


let cardScore (input: string) =
    let games = (input.Split(":")[1]).Split("|")
    let whitespaceRegex = new Regex("\s+")
    let cardDraw = Set.ofArray (whitespaceRegex.Split(games[0].Trim()))
    let winningNums = Set.ofArray (whitespaceRegex.Split(games[1].Trim()))
    let intersect = Set.intersect cardDraw winningNums
    // intersect is valid here but now I need to iterate over the card set
    if intersect.Count > 1 then pown 2 (intersect.Count - 1)
    elif intersect.Count = 1 then 1
    else 0

let testAnswer =
    System.IO.File.ReadLines "testinput.txt"
    |> List.ofSeq
    |> List.fold (fun acc elem -> cardScore (elem) + acc) 0

printfn "Test input %A" testAnswer

let solution1 =
    System.IO.File.ReadLines "input.txt"
    |> List.ofSeq
    |> List.fold (fun acc elem -> cardScore (elem) + acc) 0

printfn "Solution 1 %A" solution1