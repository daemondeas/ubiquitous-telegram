namespace Fsharp.Solutions

module Day06 =
    type Race = { time: int64; recordDistance: int64 }
    
    let rec waysToBeatRecord (raceTime: int64) (recordToBeat: int64) (chargingTime: int64) (amount: int64) =
        if chargingTime * (raceTime - chargingTime) > recordToBeat then
            waysToBeatRecord raceTime recordToBeat (chargingTime + 1L) (amount + 1L)
        elif amount > 0 then
            amount
        else
            waysToBeatRecord raceTime recordToBeat (chargingTime + 1L) amount

    let waysToWinRace race =
        waysToBeatRecord race.time race.recordDistance 1 0
        
    let firstPuzzle input =
        List.map waysToWinRace input |> List.fold (*) 1L
        
    let secondPuzzle race =
        waysToWinRace race