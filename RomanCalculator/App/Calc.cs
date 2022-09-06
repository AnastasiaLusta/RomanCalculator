namespace RomanCalculator.App;

public class Calc
{
    public void Run()
    {
        //informs that calculator is running
        Console.WriteLine("Calculator is running");
        Console.WriteLine(RomanNumber.Parse("CM"));
        Console.WriteLine(RomanNumber.Parse("CD"));
        // MORE COMMENT
    }
}