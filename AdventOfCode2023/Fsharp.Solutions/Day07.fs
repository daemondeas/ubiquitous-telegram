namespace Fsharp.Solutions

open System

module Day07 =
    type HandType = FiveOfAKind | FourOfAKind | FullHouse | ThreeOfAKind | TwoPair | Pair | HighCard
    
    let getHandType cards =
        if List.distinct cards |> List.length = 1 then
            FiveOfAKind
        elif List.distinct cards |> List.length = 2 then
            match List.filter (fun c -> c = cards[0]) cards |> List.length with
            | 1 -> FourOfAKind
            | 4 -> FourOfAKind
            | _ -> FullHouse
        elif List.distinct cards |> List.length = 3 then
            match List.filter (fun c -> c = cards[0]) cards |> List.length with
            | 3 -> ThreeOfAKind
            | 2 -> TwoPair
            | _ ->
                match List.filter (fun c -> c = cards[1]) cards |> List.length with
                | 3 -> ThreeOfAKind
                | 2 -> TwoPair
                | _ -> ThreeOfAKind
        elif List.distinct cards |> List.length = 4 then
            Pair
        else
            HighCard
            
    let handTypeValue handType =
        match handType with
        | HighCard     -> 0
        | Pair         -> 1
        | TwoPair      -> 2
        | ThreeOfAKind -> 3
        | FullHouse    -> 4
        | FourOfAKind  -> 5
        | FiveOfAKind  -> 6
            
    let rec compareCards first second =
        match first with
        | c::cs ->
            if c = (List.head second) then
                compareCards cs (List.tail second)
            else
                c - (List.head second)
        | []    -> 0
    
    let compareHands first second =
        if (getHandType first |> handTypeValue) <> (getHandType second |> handTypeValue) then
            (getHandType first |> handTypeValue) - (getHandType second |> handTypeValue)
        else
            compareCards first second
            
    let transformJokerHand hand =
        match List.filter (fun c -> c = 1) hand |> List.length with
        | 1 ->
            let jokerLessHand = List.filter (fun c -> c <> 1) hand
            if List.distinct jokerLessHand |> List.length = 2 then
                match List.filter (fun c -> c = (List.head jokerLessHand)) jokerLessHand |> List.length with
                | 1 -> List.append jokerLessHand [(List.tail jokerLessHand |> List.head)]
                | _ -> List.append jokerLessHand [(List.head jokerLessHand)]
            elif List.distinct jokerLessHand |> List.length = 3 then
                if List.filter (fun c -> c = (List.head jokerLessHand)) jokerLessHand |> List.length = 2 then
                    List.append jokerLessHand [(List.head jokerLessHand)]
                elif List.filter (fun c -> c = (List.tail jokerLessHand |> List.head)) jokerLessHand |> List.length = 2 then
                    List.append jokerLessHand [(List.tail jokerLessHand |> List.head)]
                elif List.filter (fun c -> c = (List.tail jokerLessHand |> List.tail |> List.head)) jokerLessHand |> List.length = 2 then
                    List.append jokerLessHand [(List.tail jokerLessHand |> List.tail |> List.head)]
                else
                    List.append jokerLessHand [(List.last jokerLessHand)]
            else
                List.append jokerLessHand [(List.head jokerLessHand)]
        | 2 ->
            let jokerLessHand = List.filter (fun c -> c <> 1) hand
            if List.distinct jokerLessHand |> List.length = 2 then
                match List.filter (fun c -> c = (List.head jokerLessHand)) jokerLessHand |> List.length with
                | 2 -> List.concat [jokerLessHand; [(List.head jokerLessHand)]; [(List.head jokerLessHand)]]
                | _ -> List.concat [jokerLessHand; [(List.last jokerLessHand)]; [(List.last jokerLessHand)]]
            else
                List.concat [jokerLessHand; [(List.head jokerLessHand)]; [(List.head jokerLessHand)]]
        | 3 ->
            let jokerLessHand = List.filter (fun c -> c <> 1) hand
            List.concat [jokerLessHand; [(List.head jokerLessHand)]; [(List.head jokerLessHand)]; [(List.head jokerLessHand)]]
        | 4 ->
            let jokerLessHand = List.filter (fun c -> c <> 1) hand
            List.concat [jokerLessHand; [(List.head jokerLessHand)]; [(List.head jokerLessHand)]; [(List.head jokerLessHand)]; [(List.head jokerLessHand)]]
        | _ -> hand
        
    let jokerHandValue cards =
        transformJokerHand cards |> getHandType |> handTypeValue
        
    let sortWithJokers first second =
        if (jokerHandValue first) <> (jokerHandValue second) then
            (jokerHandValue first) - (jokerHandValue second)
        else
            compareCards first second
    
    [<StructuralEquality; CustomComparison>]
    type HandWithBid =
        { hand: int list; bid: int }
        interface IComparable with
            member this.CompareTo other =
                match other with
                | :? HandWithBid as h -> (this :> IComparable<_>).CompareTo h
                | _                   -> -1
                
        interface IComparable<HandWithBid> with
            member this.CompareTo other = sortWithJokers this.hand other.hand

    let firstPuzzle input =
        List.sort input |> List.zip [1..(List.length input)] |> List.map (fun a -> (fst a) * (snd a |> (_.bid))) |> List.sum