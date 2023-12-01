namespace Inputs;

public abstract class AbstractInput<TU, TV> : IInput
{
    public long FirstPuzzle(string input) => SolveFirstPuzzle(ParseInput(input));

    public long SecondPuzzle(string input) => SolveSecondPuzzle(ParseInputTwo(input));

    protected abstract TU ParseInput(string input);

    protected abstract TV ParseInputTwo(string input);

    protected abstract long SolveFirstPuzzle(TU input);

    protected abstract long SolveSecondPuzzle(TV input);

    public abstract string TestInput { get; }
    public abstract string RealInput { get; }
}