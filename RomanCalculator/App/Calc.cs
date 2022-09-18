using System.Linq.Expressions;

namespace RomanCalculator.App;

public class Calc
{
    private readonly Resources _resources; // DI
    public Calc(Resources resources)
        => _resources = resources;
    /// <summary>
    /// Runs the calculator
    /// </summary>
    public void Run()
    {
        Console.WriteLine(_resources.GetWelcomeMessage());
        SelectCulture();
        RomanNumber res = null;
        do
        {
            Console.WriteLine(_resources.GetEnterNumberMessage());
            string userInput = Console.ReadLine() ?? "";
            try
            {
                res = EvalExpression(userInput);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        } while (res is null);
        
        Console.WriteLine(_resources.GetResultMessage(res));
    }

    /// <summary>
    /// Inputs two numbers from the console
    /// </summary>
    /// <returns>The array of Roman numbers</returns>
    public RomanNumber EvalExpression(string expression)
    {
        string[] parts = expression.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 3)
        {
            throw new ArgumentException(_resources.GetInvalidExpressionMessage());
        }

        var rn1 = new RomanNumber(RomanNumber.Parse(parts[0]));
        var rn2 = new RomanNumber(RomanNumber.Parse(parts[2]));

        return Calculate(rn1, parts[1], rn2);
    }

    /// <summary>
    /// Select language (culture) for user interface
    /// </summary>
    public void SelectCulture()
    {
        string culture = null;
        do
        {
            try
            {
                Console.WriteLine(_resources.GetCultureMessage());
                culture = Console.ReadLine();
                switch (culture)
                {
                    case "uk-UA":
                        _resources.Culture = "uk-UA";
                        break;
                    case "en-US":
                        _resources.Culture = "en-US";
                        break;
                    default:
                        throw new ArgumentException(_resources.GetInvalidCultureMessage());
                        culture = null;
                        break;
                }
            }
            catch
            {
                Console.WriteLine(_resources.GetInvalidCultureMessage());
            }
        } while (culture == null);
    }
    /// <summary>
    /// Apply calculation of two roman numbers 
    /// </summary>
    /// <param name="num1">first roman number</param>
    /// <param name="num2">second roman number</param>
    public RomanNumber Calculate(RomanNumber num1, string op, RomanNumber num2)
    {
        var operation = op;
        RomanNumber res = null;
        switch (operation)
        {
            case "+":
                res = RomanNumber.Add(num1, num2);
                break;
            case "-":
                res = RomanNumber.Subtract(num1, num2);
                break;
            case "*":
                res = RomanNumber.Mult(num1, num2);
                break;
            case "/":
                res = RomanNumber.Div(num1, num2);
                break;
            default:
                throw new ArgumentException(_resources.GetInvalidOperationMessage());
        }

        return res;
    }
}