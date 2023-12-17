namespace Fsharp.Solutions

open System.Collections.Immutable

module Day10 =
    type Direction = North | East | South | West
    type Pipe = NorthSouth | EastWest | NorthEast | NorthWest | SouthWest | SouthEast
    
    let nextPositionAndDirection (position: int*int) (pipe: Pipe) (enteringDirection: Direction) =
        if pipe = NorthSouth && enteringDirection = South || pipe = SouthWest && enteringDirection = East || pipe = SouthEast && enteringDirection = West then
            (((fst position), ((snd position) + 1)), South)
        elif pipe = NorthSouth && enteringDirection = North || pipe = NorthEast && enteringDirection = West || pipe = NorthWest && enteringDirection = East then
            (((fst position), ((snd position) - 1)), North)
        elif pipe = EastWest && enteringDirection = West || pipe = NorthWest && enteringDirection = South || pipe = SouthWest && enteringDirection = North then
            ((((fst position) - 1), (snd position)), West)
        else
            ((((fst position) + 1), (snd position)), East)

    let rec pipeLength startPosition position (pipes: ImmutableDictionary<int*int, Pipe>) currentLength direction =
        if (startPosition = position) then
            currentLength
        else
            nextPositionAndDirection position (pipes[position]) direction |>
                (fun p -> pipeLength startPosition (fst p) pipes (currentLength + 1) (snd p))
                
    let southFacing = [NorthSouth; SouthWest; SouthEast]
    let northFacing = [NorthSouth; NorthWest; NorthEast]
    let eastFacing = [EastWest; NorthEast; SouthEast]
                
    let startingDirectionAndPosition position (pipes: ImmutableDictionary<int*int, Pipe>) =
        if pipes.ContainsKey ((fst position), ((snd position) - 1)) && List.contains pipes[((fst position), ((snd position) - 1))] southFacing then
            (((fst position), ((snd position) - 1)), North)
        elif pipes.ContainsKey ((fst position), ((snd position) + 1)) && List.contains pipes[((fst position), ((snd position) + 1))] northFacing then
            (((fst position), ((snd position) + 1)), South)
        elif pipes.ContainsKey (((fst position) - 1), (snd position)) && List.contains pipes[(((fst position) - 1), (snd position))] eastFacing then
            ((((fst position) - 1), (snd position)), West)
        else
            ((((fst position) + 1), (snd position)), East)
            
    let firstPuzzle startingPoint pipes =
        startingDirectionAndPosition startingPoint pipes |> (fun p -> pipeLength startingPoint (fst p) pipes 1 (snd p)) |> (fun l -> l / 2)
