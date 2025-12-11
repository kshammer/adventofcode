// Starting is 50 max is 99

let input = System.IO.File.ReadLines "input.txt" |> List.ofSeq

let calculate (current: int) (update: string) : int =
    let minMoves = System.Int32.Parse(update.[1..]) % 100

    if update.[0] = 'R' then
        current + minMoves
    else
        current - minMoves

let normalize (value: int) : int =
    if value < 0 then value + 100
    elif value > 99 then value - 100
    else value

let folder (sumAcc, curVal) item =
    let cool = calculate curVal item
    let newVal = normalize cool
    if newVal = 0 then sumAcc + 1, newVal else sumAcc, newVal

let zeroes, _ = List.fold folder (0, 50) input

printfn "Output %d" zeroes
