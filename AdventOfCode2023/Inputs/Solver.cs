namespace Inputs;

public class Solver
{
    private readonly Dictionary<int, IInput> _inputSolvers;

    public Solver(Dictionary<int, IInput> inputSolvers)
    {
        _inputSolvers = inputSolvers;
    }

    public long Solve(int day, bool firstPuzzle, bool testInput) =>
        _inputSolvers.TryGetValue(day, out var inputSolver)
            ? firstPuzzle
                ? inputSolver.FirstPuzzle(testInput ? inputSolver.TestInput : inputSolver.RealInput)
                : inputSolver.SecondPuzzle(testInput ? inputSolver.SecondTestInput : inputSolver.RealInput)
            : throw new ArgumentOutOfRangeException(nameof(day));
}