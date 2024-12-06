open System

type NumberCoord =
    { num: int
      neighbors: (int * int) list // [(1,2)] tuple list
      mutable visited: bool }

let testInput =
    [| "467..114.."
       "...*......"
       "..35..633."
       "......#..."
       "617*......"
       ".....+.58."
       "..592....."
       "......755."
       "...$.*...."
       ".664.598.." |]


let rec neighborGenerator (x: int list, y: int, acc: (int * int) list) =
    if x.IsEmpty then
        acc
    else
        let num = x.Head

        neighborGenerator (
            x.Tail,
            y,
            // idk why I have to use list join instead of ::
            // Also creates a lot of duplicates but w/e
            List.append
                [ 
                  (num + 1, y + 1)
                  (num + 1, y)
                  (num + 1, y - 1)
                  (num, y + 1)
                  (num, y - 1)
                  (num - 1, y + 1)
                  (num - 1, y - 1)
                  (num - 1, y) ]
                acc
        )


let rec stringParser (input: string, x: int, y: int, acc: NumberCoord list) =
    try
        let numFirstChar = Seq.findIndex Char.IsNumber input
        let subString = input[numFirstChar..]
        // figure out if the number is at the end of the line
        // try using a match statement to break the flow 
        let numLastChar = Seq.findIndex (Char.IsNumber >> not) subString
        let num = subString[.. numLastChar - 1] |> int
        printfn "Num %A first char %A last char %A  x %A" num numFirstChar numLastChar x
        stringParser (
            subString[numLastChar..],
            (numFirstChar + numLastChar) + x,
            y,
            { num = num
              neighbors =
                neighborGenerator (
                    // this is the bug
                    [ numFirstChar + x .. (numFirstChar + (numLastChar - 1) + x) ],
                    y,
                    List.Empty
                )
              visited = false }
            :: acc
        )
    with :? Collections.Generic.KeyNotFoundException ->
        acc

let rec findSymbols (input: string, x: int, y: int, acc: (int * int) list) =
    try
        let symbolLocation =
            Seq.findIndex (fun c -> (Char.IsNumber c |> not) && (c = '.' |> not)) input

        let subString = input[symbolLocation + 1 ..]
        // printfn "Symbol loc %A substring %A" symbolLocation subString
        findSymbols (subString, symbolLocation + 1 + x, y, (symbolLocation + x, y) :: acc)
    with :? Collections.Generic.KeyNotFoundException ->
        acc



let getNumberList (input) =
    input
    |> Array.fold (fun (acc, i) elem -> List.append (stringParser (elem, 0, i, List.Empty)) acc, i + 1) ([], 0)

let getSymbolLocations (input) =
    input
    |> Array.fold (fun (acc, i) elem -> (List.append (findSymbols (elem, 0, i, List.Empty)) acc, i + 1)) ([], 0)

let matchFunc (numCoord: NumberCoord, coord: int * int) =
    List.exists (fun elem -> 
        (elem = coord) && (numCoord.visited = false)) numCoord.neighbors

let solution (input) =
    let symbolLocation, _ = getSymbolLocations input
    let numberList, _ = getNumberList input

    List.fold
        (fun sum symbolLoc ->
            let validNumbers = List.filter (fun elem -> matchFunc (elem, symbolLoc)) numberList
            printfn "Valid Numbers %A" validNumbers
            let partialcSum =
                List.fold
                    (fun partialSum valNum ->
                        valNum.visited <- true
                        partialSum + valNum.num)
                    0
                    validNumbers

            sum + partialcSum)
        0
        symbolLocation

// let testAnswer = solution testInput

// let testSymbols, _ = getSymbolLocations testInput

// let testNumbers, _ = getNumberList testInput

// printfn "test %A" testAnswer

// printfn "test SYmbols %A " testSymbols

// printfn "test numbers %A" testNumbers

// let debug = [| 
//                               ".......505............61..140..........435..........*691......*........852..........571....408..............12......80.......228...109......";
//                               ".........................*.........207...*..24................402.=...........................@....................../...162................"
//                               |]
let debug = [|"*22"|]
let debugAnswer = solution debug

printfn "debug %A" debugAnswer
let debugSymbolLocs,  _ = getSymbolLocations debug
printfn "Debug symbols %A" debugSymbolLocs
let debugNumbers, _ = getNumberList debug
printfn "Debug numbers %A" debugNumbers

// let actualInputSolution = System.IO.File.ReadLines "input.txt" |> Array.ofSeq

// let answer1 = solution actualInputSolution

// printfn "problem 1 %A" answer1
