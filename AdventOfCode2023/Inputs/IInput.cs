namespace Inputs;

public interface IInput
{
    long FirstPuzzle(string input);

    long SecondPuzzle(string input);
    
    string TestInput { get; }
    
    string RealInput { get; }
}