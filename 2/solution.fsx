// Max 12 red, 13 green, 14 blue

type BlockMatch = { red: int; blue: int; green: int }
type Game = { id: int; matches: BlockMatch list }

let testInput =
    [ "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green"
      "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue"
      "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red"
      "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red"
      "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green" ]

let maxBlockMatch = { red = 12; green = 13; blue = 14 }

let getId (input: string) =
    ((input.Split(":")[0]).Split(" ")[1]) |> int

let getMatches (input: string) =
    (input.Split(":")[1]).Split(";") |> Array.toList

let matchToRecord (input: string) =
    let temp = { red = 0; blue = 0; green = 0 }

    input.Split(",")
    |> Array.toList
    |> List.fold
        (fun acc elem ->
            let trimmed = elem.Trim()
            let blockType = trimmed.Split(" ")[1]
            let blockCount = trimmed.Split(" ")[0] |> int

            match blockType with
            | "blue" -> { acc with blue = blockCount }
            | "red" -> { acc with red = blockCount }
            | "green" -> { acc with green = blockCount }
            | _ -> acc)
        { red = 0; blue = 0; green = 0 }

let inputToGame (input: string) =
    { id = getId (input)
      matches = getMatches (input) |> List.map matchToRecord }

let validateMatch (input: BlockMatch) =
    if input.blue > maxBlockMatch.blue then false
    elif input.green > maxBlockMatch.green then false
    elif input.red > maxBlockMatch.red then false
    else true

let solution1 (input: string list) =
    input
    |> List.map inputToGame
    |> List.filter (fun game -> List.forall (validateMatch) game.matches)
    |> List.fold (fun acc elem -> acc + elem.id) 0

let testInputSolution = solution1 testInput

printfn "test input problem1 %A" testInputSolution

let actualInput = System.IO.File.ReadLines "input.txt" |> List.ofSeq

let actualInputSolution = solution1 actualInput

printfn "actual input problem1 %A" actualInputSolution