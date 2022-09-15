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
        var rns = InputNumbers();
        Calculate(rns[0], rns[1]);
    }
    /// <summary>
    /// Inputs two numbers from the console
    /// </summary>
    /// <returns>The array of Roman numbers</returns>
    private RomanNumber[] InputNumbers()
    {
        var rns = new RomanNumber[] { null!, null! };
        for (int i = 0; i < 2; i++)
        {
            do
            {
                try
                {
                    Console.WriteLine(_resources.GetEnterNumberMessage());
                    rns[i] = new RomanNumber(RomanNumber.Parse(Console.ReadLine()!));
                }
                catch
                {
                    Console.WriteLine(_resources.GetInvalidNumberMessage());
                }
            } while (rns[i] == null);
        }

        return rns;
    }
    /// <summary>
    /// Select language (culture) for user interface
    /// </summary>
    private void SelectCulture()
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
                        Console.WriteLine(_resources.GetInvalidCultureMessage());
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
    private void Calculate(RomanNumber num1, RomanNumber num2)
    {
        Console.WriteLine(_resources.GetEnterOperationMessage());
        var operation = Console.ReadLine();
        switch (operation)
        {
            case "+":
                Console.WriteLine(_resources.GetResultMessage(RomanNumber.Add(num1,num2)));
                break;
            default:
                Console.WriteLine(_resources.GetInvalidOperationMessage());
                break;
        }
    }
}