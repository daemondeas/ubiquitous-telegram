namespace Fsharp.Solutions

module Day03 =
    type number = {
        value : int
        line  : int
        xLow  : int
        xHigh : int
    }

    type position = {
        x : int
        y : int
    }

    let getAdjacent n =
        List.map (fun x -> { x = x; y = (n.line - 1) }) [(n.xLow - 1)..(n.xHigh + 1)]
        |> List.append [{ x = (n.xLow - 1); y = n.line}; { x = (n.xHigh + 1); y = n.line}]
        |> List.append (List.map (fun x -> { x = x; y = (n.line + 1) }) [(n.xLow - 1)..(n.xHigh + 1)])

    let rec listContains ps ns =
        match ns with
        | x::xs -> List.contains x ps || listContains xs ps
        | []    -> false

    let isPartNumber symbols n =
        getAdjacent n |> listContains symbols

    let firstPuzzle numbers symbols =
        List.filter (isPartNumber symbols) numbers |> List.map (_.value) |> List.sum