open System

type NumberCoord =
    { num: int
      neighbors: (int * int) list } // [(1,2)] tuple list

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
                [ (num - 1, y + 1)
                  (num, y + 1)
                  (num + 1, y + 1)
                  (num + 1, y)
                  (num + 1, y - 1)
                  (num, y - 1)
                  (num - 1, y - 1)
                  (num - 1, y) ]
                acc
        )


let rec stringParser (input: string, x: int, y: int, acc: NumberCoord list) =
    try
        let numFirstChar = Seq.findIndex Char.IsNumber input
        let subString = input[numFirstChar..]
        let numLastChar = Seq.findIndex (Char.IsNumber >> not) subString
        let num = subString[.. numLastChar - 1] |> int

        stringParser (
            subString[numLastChar..],
            numLastChar + x,
            y,
            { num = num
              neighbors =
                neighborGenerator (
                    // this is the bug
                    [ numFirstChar + x .. (numFirstChar + (numLastChar - 1) + x) ],
                    y,
                    List.Empty
                ) }
            :: acc
        )
    with :? Collections.Generic.KeyNotFoundException ->
        acc

let rec findSymbols (input: string, x: int, y: int, acc: (int * int) list) =
    try
        let symbolLocation =
            Seq.findIndex (fun c -> (Char.IsNumber c |> not) && (c = '.' |> not)) input

        let subString = input[symbolLocation + 1 ..]
        findSymbols (subString, symbolLocation + 1, y, (symbolLocation + x, y) :: acc)
    with :? Collections.Generic.KeyNotFoundException ->
        acc




// TODO can't use acc .length 
let numberList =
    testInput
    |> Array.fold (fun acc elem -> List.append (stringParser (elem, 0, acc.Length, List.Empty)) acc) []

let symbols =
    testInput
    |> Array.fold (fun acc elem -> List.append (findSymbols (elem, 0, acc.Length, List.Empty)) acc) []

printfn "%A" symbols