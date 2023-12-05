namespace Fsharp.Solutions

open System.Collections.Generic

module Day04 =
    type Card =
     {
        cardNumber     : int
        winningNumbers : Set<int>
        hadNumbers     : Set<int>
     }
     
    let numberOfMatches card =
        Set.intersect card.winningNumbers card.hadNumbers |> Set.count

    let cardPoints card =
        numberOfMatches card |> (fun p ->
            match p with
            | 0 -> 0
            | n -> pown 2 (n - 1))
        
    let firstPuzzle input =
        List.map cardPoints input |> List.sum
        
    let newCopies card =
        if numberOfMatches card <> 0 then
            [(card.cardNumber + 1)..(card.cardNumber + (numberOfMatches card))]
        else
            []
        
    let rec fillNewCards amount (copies: Dictionary<int, int>) indices =
        match indices with
        | i::is ->
            copies[i] <- (copies[i] + amount)
            fillNewCards amount copies is
        | []    -> copies
        
    let rec amountOfCardsDict cards copies =
        match cards with
        | c::cs ->
            let matches = numberOfMatches c
            if matches <> 0 then
                amountOfCardsDict cs (fillNewCards copies[c.cardNumber] copies (newCopies c))
            else
                amountOfCardsDict cs copies
        | []    -> Seq.map id copies.Values |> Seq.sum
        
    let rec initCopies cards (copies: Dictionary<int, int>) =
        match cards with
        | c::cs ->
            copies.Add(c.cardNumber, 1)
            initCopies cs copies
        | []    -> copies
        
    let secondPuzzle input =
        initCopies input (Dictionary<int, int>()) |> (amountOfCardsDict input)