namespace Fsharp.Solutions

open System.Text.RegularExpressions

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

    let rx = Regex(@"\d|(one)|(two)|(three)|(four)|(five)|(six)|(seven)|(eight)|(nine)", RegexOptions.Compiled)

    let convertToNumber s =
        match s with
        | "0"     -> 0
        | "1"     -> 1
        | "one"   -> 1
        | "2"     -> 2
        | "two"   -> 2
        | "3"     -> 3
        | "three" -> 3
        | "4"     -> 4
        | "four"  -> 4
        | "5"     -> 5
        | "five"  -> 5
        | "6"     -> 6
        | "six"   -> 6
        | "7"     -> 7
        | "seven" -> 7
        | "8"     -> 8
        | "eight" -> 8
        | "9"     -> 9
        | "nine"  -> 9

    let numberFromLineRegex line =
        let matches = rx.Matches(line)
        (10 * (convertToNumber ((Seq.head matches) |> (_.Value)))) + (convertToNumber ((Seq.last matches) |> (_.Value)))

    let secondPuzzle (input: string list) =
        List.map numberFromLineRegex input |> List.sum