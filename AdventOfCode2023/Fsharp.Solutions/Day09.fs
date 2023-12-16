namespace Fsharp.Solutions

module Day09 =
    let rec getDifferenceList (previous: int64) (values: int64 list) (result: int64 list) =
        match values with
        | v::vs -> getDifferenceList v vs (List.append result [(v - previous)])
        | []    -> result

    let getDifferenceListWrapper values =
        getDifferenceList (List.head values) (List.tail values) []
        
    let rec getNextValue values =
        if (List.length values) = (List.filter (fun v -> v = 0L) values |> List.length) then
            0L
        else
            (List.last values) + (getNextValue (getDifferenceListWrapper values))
            
    let firstPuzzle input =
        List.map getNextValue input |> List.sum
        
    let rec getPreviousValue values =
        if (List.length values) = (List.filter (fun v -> v = 0L) values |> List.length) then
            0L
        else
            (List.head values) - (getPreviousValue (getDifferenceListWrapper values))
            
    let secondPuzzle input =
        List.map getPreviousValue input |> List.sum