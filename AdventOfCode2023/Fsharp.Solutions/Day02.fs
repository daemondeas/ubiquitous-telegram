namespace Fsharp.Solutions

module Day02 =
    type Round = {
        redCubes   : int
        greenCubes : int
        blueCubes  : int
    }

    type Game = {
        id     : int
        rounds : Round list
    }

    let isRoundValid availableRed availableGreen availableBlue round =
        round.redCubes <= availableRed && round.greenCubes <= availableGreen && round.blueCubes <= availableBlue

    let isGameValid availableRed availableGreen availableBlue game =
        List.map (isRoundValid availableRed availableGreen availableBlue) game.rounds |> List.fold (&&) true

    let firstPuzzle input =
        List.filter (isGameValid 12 13 14) input |> List.map (fun g -> g.id) |> List.sum

    let rec roundsPower red green blue rounds =
        match rounds with
        | x::xs -> roundsPower (max red x.redCubes) (max green x.greenCubes) (max blue x.blueCubes) xs
        | []    -> red * green * blue

    let gamePower game = roundsPower 0 0 0 game.rounds

    let secondPuzzle input =
        List.map gamePower input |> List.sum