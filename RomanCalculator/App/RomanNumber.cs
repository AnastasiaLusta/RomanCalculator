namespace RomanCalculator.App;

public class RomanNumber
{
    public static int Parse(string romanNumber)
    {
        //dictionary with the roman numbers and their values
        var digits = new Dictionary<char, int>()
        {
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 }
        };
        var result = 0;
        var previous = 0;
    
        foreach (var number in romanNumber)
        {
            var current = digits[number];
            if (current > previous)
            {
                result -= previous;
                result += current - previous;
            }
            else
            {
                result += digits[number];
            }

            previous = current;
        }

        return result;
    }
}