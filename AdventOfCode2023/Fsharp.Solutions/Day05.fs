namespace Fsharp.Solutions

module Day05 =
    type RangeMap =
        {
          source      : int64
          destination : int64
          length      : int64
        }
        
    let rec getDestination maps source =
        match maps with
        | m::ms ->
            if m.source <= source && source <= m.source + m.length then
                m.destination + source - m.source
            else
                getDestination ms source
        | []    -> source
        
    let firstPuzzle seeds seedToSoilMaps soilToFertilizerMaps fertilizerToWaterMaps waterToLightMaps lightToTemperatureMaps temperatureToHumidityMaps humidityToLocationMaps =
        List.map (getDestination seedToSoilMaps) seeds
        |> List.map (getDestination soilToFertilizerMaps)
        |> List.map (getDestination fertilizerToWaterMaps)
        |> List.map (getDestination waterToLightMaps)
        |> List.map (getDestination lightToTemperatureMaps)
        |> List.map (getDestination temperatureToHumidityMaps)
        |> List.map (getDestination humidityToLocationMaps)
        |> List.min
        
    let rec actualSeeds (input: int64 list) result =
        if input = [] then
            result
        else
            actualSeeds (List.skip 2 input) (List.append result [input[0]..(input[0] + input[1] - 1L)])

    let secondPuzzle seeds seedToSoilMaps soilToFertilizerMaps fertilizerToWaterMaps waterToLightMaps lightToTemperatureMaps temperatureToHumidityMaps humidityToLocationMaps =
        actualSeeds seeds [] |> (fun s -> firstPuzzle s seedToSoilMaps soilToFertilizerMaps fertilizerToWaterMaps waterToLightMaps lightToTemperatureMaps temperatureToHumidityMaps humidityToLocationMaps)