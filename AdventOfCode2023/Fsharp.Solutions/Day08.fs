namespace Fsharp.Solutions

open System.Collections.Immutable

module Day08 =
    let nextNode (map: ImmutableDictionary<string, string * string>) current instruction =
        match instruction with
        | 'L' -> map[current] |> fst
        | 'R' -> map[current] |> snd
    
    let rec stepsTo goal map current instructions remainingInstructions amountOfSteps =
        if current = goal then
            amountOfSteps
        else
            match remainingInstructions with
            | r::rs -> stepsTo goal map (nextNode map current r) instructions rs (amountOfSteps + 1)
            | []    -> stepsTo goal map current instructions instructions amountOfSteps

    let firstPuzzle instructions map =
        stepsTo "ZZZ" map "AAA" instructions instructions 0