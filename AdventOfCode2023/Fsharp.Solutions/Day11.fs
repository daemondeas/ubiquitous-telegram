namespace Fsharp.Solutions

module Day11 =
    let isLineEmpty line galaxies =
        List.filter (fun g -> (snd g) = line) galaxies |> List.length |> (fun l -> l = 0)

    let isColumnEmpty column galaxies =
        List.filter (fun g -> (fst g) = column) galaxies |> List.length |> (fun l -> l = 0)
        
    let expandLine expandBy galaxies =
        List.map (fun g -> ((fst g), ((snd g) + expandBy))) galaxies
        
    let expandColumn expandBy galaxies =
        List.map (fun g -> (((fst g) + expandBy), (snd g))) galaxies
        
    let rec expandLines expandBy currentLine lastLine galaxies =
        if (currentLine = lastLine) then
            galaxies
        else
            if isLineEmpty currentLine galaxies then
                expandLines expandBy (currentLine + expandBy + 1L) (lastLine + expandBy) (List.append (List.filter (fun l -> (snd l) < currentLine) galaxies) (List.filter (fun l -> (snd l) > currentLine) galaxies |> (expandLine expandBy)))
            else
                expandLines expandBy (currentLine + 1L) lastLine galaxies
                
    let rec expandColumns expandBy currentColumn lastColumn galaxies =
        if (currentColumn = lastColumn) then
            galaxies
        else
            if isColumnEmpty currentColumn galaxies then
                expandColumns expandBy (currentColumn + expandBy + 1L) (lastColumn + expandBy) (List.append (List.filter (fun c -> (fst c) < currentColumn) galaxies) (List.filter (fun c -> (fst c) > currentColumn) galaxies |> (expandColumn expandBy)))
            else
                expandColumns expandBy (currentColumn + 1L) lastColumn galaxies
                
    let expandMap expandBy galaxies =
        expandLines expandBy 0 (List.map snd galaxies |> List.max) galaxies |> (expandColumns expandBy 0 (List.map fst galaxies |> List.max))
        
    let manhattanDistance first second =
        (abs ((fst first) - (fst second))) + (abs ((snd first) - (snd second)))
        
    let manhattanDistances node otherNodes =
        List.map (manhattanDistance node) otherNodes
        
    let rec allManhattanDistances currentDistances galaxies =
        match galaxies with
        | g::gs -> allManhattanDistances (List.append currentDistances (manhattanDistances g gs)) gs
        | []    -> currentDistances
        
    let firstPuzzle (galaxies: (int64 * int64) list) =
        expandMap 1L galaxies |> (allManhattanDistances []) |> List.sum
        
    let secondPuzzle (galaxies: (int64 * int64) list) =
        expandMap 999999L galaxies |> (allManhattanDistances []) |> List.sum