namespace Fsharp.Solutions

module Day12 =
    type Spring = Broken | Functioning | Unknown
    
    type Row = {
        springs : Spring list
        groups  : int list
    }
    
    let rec nextGroupLength length springs =
        if length > 0 then
            match springs with
            | s::ss ->
                match s with
                | Functioning -> (length, ss)
                | _           -> nextGroupLength (length + 1) ss
            | []    -> (length, springs)
        else
            match springs with
            | s::ss ->
                match s with
                | Functioning -> nextGroupLength length ss
                | _           -> nextGroupLength (length + 1) ss
            | []    -> (length, springs)
            
    let isMatch next springs =
        nextGroupLength 0 springs |> (fun s -> (next = (fst s), snd s))
        
    let rec matches lengths springs =
        match lengths with
        | l::ls -> isMatch l springs |> (fun m -> (fst m) && (matches ls (snd m)))
        | []    -> List.filter (fun s -> s <> Functioning) springs |> List.length |> (fun l -> l = 0)
        
    // let rec possibleSprings springs =
    //     match springs with
    //     | s::ss -> 
    //
    // let amountOfArrangements row = 99