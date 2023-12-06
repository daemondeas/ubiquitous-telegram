using Inputs;

Console.WriteLine("Please choose a day:");
var input = Console.ReadLine();

if (!int.TryParse(input, out var day))
{
    Console.WriteLine("You have to choose a day by it's number");
    return;
}

if (day is < 1 or > 25)
{
    Console.WriteLine("The day must be within the range 1-25");
    return;
}

Console.WriteLine("First (1) or second (2) puzzle?");
var firstPuzzle = Console.ReadLine() == "1";

Console.WriteLine("Test input (t) or real input (r)?");
var useTestInput = Console.ReadLine() == "t";

var solver = new Solver(
    new Dictionary<int, IInput>
    {
        { 1, new Input01() },
        { 2, new Input02() },
        { 3, new Input03() },
        { 4, new Input04() },
        { 5, new Input05() },
        { 6, new Input06() },
        // { 7, new Input07() },
        // { 8, new Input08() },
        // { 9, new Input09() },
        // { 10, new Input10() },
        // { 11, new Input11() },
        // { 12, new Input12() },
        // { 13, new Input13() },
        // { 14, new Input14() },
        // { 15, new Input15() },
        // { 16, new Input16() },
        // { 17, new Input17() },
        // { 18, new Input18() },
        // { 19, new Input19() },
        // { 20, new Input20() },
        // { 21, new Input21() },
        // { 22, new Input22() },
        // { 23, new Input23() },
        // { 24, new Input24() },
        // { 25, new Input25() },
    });

var result = solver.Solve(day, firstPuzzle, useTestInput);

Console.WriteLine($"Result for day {day}, puzzle {(firstPuzzle ? '1' : '2')}, {(useTestInput ? "test" : "real")} input is \n{result}");