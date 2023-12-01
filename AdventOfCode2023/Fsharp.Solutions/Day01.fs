namespace Fsharp.Solutions

module Day01 =
    let rec firstNumber (chars: char list) =
        match chars with
        | x::xs ->
            if x >= '0' && x <= '9' then
                (int x) - (int '0')
            else
                firstNumber xs
        | []    -> raise (new System.Exception("bad line"))

    let numberFromLine (line: string) =
        let chars = Seq.toList line
        (10 * (firstNumber chars)) + (firstNumber (List.rev chars))

    let firstPuzzle (input: string list) =
        List.map numberFromLine input |> List.sum