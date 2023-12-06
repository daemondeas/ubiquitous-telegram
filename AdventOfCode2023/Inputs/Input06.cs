using Fsharp.Solutions;
using Microsoft.FSharp.Collections;

namespace Inputs;

public class Input06 : AbstractInput<FSharpList<Day06.Race>, Day06.Race>
{
    protected override FSharpList<Day06.Race> ParseInput(string input)
    {
        var rows = input.Split('\n');
        var result = new List<Day06.Race>();
        var times = RowToNumbers(rows[0]).ToArray();
        var distances = RowToNumbers(rows[1]).ToArray();
        for (var i = 0; i < times.Length; i++)
        {
            result.Add(new Day06.Race(times[i], distances[i]));
        }

        return ListModule.OfSeq(result);
    }

    protected override Day06.Race ParseInputTwo(string input)
    {
        var rows = input.Split('\n');
        return new Day06.Race(RowToNumber(rows[0]), RowToNumber(rows[1]));
    }

    protected override long SolveFirstPuzzle(FSharpList<Day06.Race> input) => Day06.firstPuzzle(input);

    protected override long SolveSecondPuzzle(Day06.Race input) => Day06.secondPuzzle(input);

    private static IEnumerable<long> RowToNumbers(string row) =>
        row.Split(':')[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse);

    private static long RowToNumber(string row) =>
        long.Parse(
            row.Split(':')[1]
                .Trim()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Aggregate((sa, sb) => $"{sa}{sb}"));

    public override string TestInput =>
        @"Time:      7  15   30
Distance:  9  40  200";

    public override string RealInput =>
        @"Time:        54     94     65     92
Distance:   302   1476   1029   1404";
}