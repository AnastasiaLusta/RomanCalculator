namespace RomanCalculator.App;

public class Calc
{
    public void Run()
    {
        Console.WriteLine("Calculator is running");
        Console.WriteLine(RomanNumber.Parse("CM"));
        Console.WriteLine(RomanNumber.Parse("CD"));
    }
}