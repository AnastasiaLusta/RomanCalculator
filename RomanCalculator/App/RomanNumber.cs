using System.Net.Sockets;

namespace RomanCalculator.App;

public record RomanNumber
{
    private const char ZeroDigit = 'N';
    private int _value;
    public static Resources Resources { get; set; } = null!;

    public int Value
    {
        get => _value;
        set => _value = value;
    }

    public RomanNumber(int v = 0)
    {
        _value = v;
    }

    public static int Parse(string romanNumber)
    {
        //dictionary with the roman numbers and values
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
        var negative = false;
        var result = 0;
        var previous = 0;
        if (string.IsNullOrEmpty(romanNumber))
        {
            throw new ArgumentNullException(Resources.GetEmptyStringMessage());
        }

        if (romanNumber == ZeroDigit.ToString())
        {
            return 0;
        }

        // throw new ArgumentException of N is in the number with other letters
        if (romanNumber.Contains(ZeroDigit) && romanNumber.Length > 1)
        {
            throw new ArgumentException(Resources.GetMispalcedNMessage());
        }

        // checks if it contains 
        foreach (var number in romanNumber)
        {
            if (!digits.ContainsKey(number))
            {
                if (number == '-')
                {
                    if (romanNumber.Length == 1)
                    {
                        throw new ArgumentException(Resources.GetInvalidCharMessage(number));
                    }

                    negative = true;
                    continue;
                }

                throw new ArgumentException(Resources.GetInvalidCharMessage((number)));
            }

            // get value of the number
            var current = digits[number];
            // check if the current number is bigger than the previous one
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

        // changes the result to negative if it contains -
        if (negative)
        {
            result = result * -1;
        }

        return result;
    }

    public override string ToString()
    {
        if (_value == 0)
        {
            return ZeroDigit.ToString();
        }

        var num = _value;
        var res = "";
        var digits = new Dictionary<int, string>()
        {
            { 1000, "M" },
            { 900, "CM" },
            { 500, "D" },
            { 400, "CD" },
            { 100, "C" },
            { 90, "XC" },
            { 50, "L" },
            { 40, "XL" },
            { 10, "X" },
            { 9, "IX" },
            { 5, "V" },
            { 4, "IV" },
            { 1, "I" }
        };

        if (num < 0)
        {
            res += "-";
            num = num * -1;
        }

        foreach (var digit in digits)
        {
            while (num >= digit.Key)
            {
                res += digit.Value;
                num -= digit.Key;
            }
        }

        return res;
    }

    private RomanNumber(object obj)
    {
        if (obj is null)
            throw new ArgumentException(Resources.GetInvalidTypeMessage(nameof(obj)));
        if (obj is int val) Value = val;
        else if (obj is string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentNullException(Resources.GetEmptyStringMessage());
            Value = Parse(str);
        }
        else if (obj is RomanNumber rn) Value = rn.Value;
        else throw new ArgumentException(Resources.GetInvalidTypeMessage(obj.GetType().Name));
    }

    // overloading for RomanNumber object and int (not static)
    public static RomanNumber Add(object obj1, object obj2) // fabrice method Add for all types 
        => new RomanNumber(obj1).Add(new RomanNumber(obj2));


    public RomanNumber Add(int other) // Add method overloading for int (not static)
        => new RomanNumber(Value + other);


    public RomanNumber Add(string other) // Add method overloading for string (not static)
        => (other == "-" && other.Length == 1)
            ? throw new ArgumentException(Resources.GetInvalidCharMessage('-'))
            : (string.IsNullOrEmpty(other))
                ? throw new ArgumentNullException(Resources.GetEmptyStringMessage())
                : new RomanNumber(_value + Parse(other));

    public RomanNumber Add(RomanNumber a) // Add method overloading for RomanNumber (not static)
        => (a is null)
            ? throw new ArgumentNullException(Resources.GetEmptyStringMessage())
            : new RomanNumber(_value + a.Value);

    public static RomanNumber Add(int num1, int num2) // overloading for two ints (static)
        => new RomanNumber(num1 + num2);

    public static RomanNumber Add(string num1, string num2) // overloading for two strings (static)
        => (num1 is null || num2 is null)
            ? throw new ArgumentNullException()
            : new RomanNumber(RomanNumber.Parse(num1)).Add(new RomanNumber(RomanNumber.Parse(num2)));

    public static RomanNumber Add(RomanNumber num1, RomanNumber num2) // overloading for two RomanNumbers (static)
        => (num1 is null || num2 is null)
            ? throw new ArgumentNullException()
            : num1.Add(num2);

    public static RomanNumber Add(RomanNumber num1, string num2) // overloading for RomanNumber and string (static)
        => (num1 is null || num2 is null)
            ? throw new ArgumentNullException()
            : new RomanNumber(num1.Add(new RomanNumber(RomanNumber.Parse(num2))));

    public static RomanNumber Add(string num1, RomanNumber num2) // overloading for string and RomanNumber (static)
        => (num1 is null || num2 is null)
            ? throw new ArgumentNullException()
            : new RomanNumber(new RomanNumber(RomanNumber.Parse(num1)).Add(num2));

    public static RomanNumber Add(RomanNumber num1, int num2) // overloading for RomanNumber and int (static)
        => (num1 is null)
            ? throw new ArgumentNullException()
            : new RomanNumber(num1.Add(new RomanNumber(num2)));

    public static RomanNumber Add(int num1, RomanNumber num2) // overloading for int and RomanNumber (static)
        => (num2 is null)
            ? throw new ArgumentNullException()
            : new RomanNumber(new RomanNumber(num1).Add(num2));

    public static RomanNumber Add(int num1, string num2) // overloading for int and string (static)
        => (num2 is null)
            ? throw new ArgumentNullException()
            : new RomanNumber(new RomanNumber(num1).Add(new RomanNumber(RomanNumber.Parse(num2))));


    public static RomanNumber Add(string num1, int num2) // overloading for string and int (static)
        => (num1 is null)
            ? throw new ArgumentNullException()
            : new RomanNumber(new RomanNumber(RomanNumber.Parse(num1)).Add(new RomanNumber(num2)));


    public static RomanNumber Subtract(object obj1, object obj2) // fabrice method Subtract for all types 
        => new RomanNumber(obj1).Subtract(new RomanNumber(obj2));


    public RomanNumber Subtract(int other) // Subtract method overloading for int (not static)
        => new RomanNumber(Value - other);


    public RomanNumber Subtract(string other) // Subtract method overloading for string (not static)
        => (other == "-" && other.Length == 1)
            ? throw new ArgumentException(Resources.GetInvalidCharMessage('-'))
            : (string.IsNullOrEmpty(other))
                ? throw new ArgumentNullException(Resources.GetEmptyStringMessage())
                : new RomanNumber(_value - Parse(other));

    public RomanNumber Subtract(RomanNumber a) // Subtract method overloading for RomanNumber (not static)
        => (a is null)
            ? throw new ArgumentNullException(Resources.GetEmptyStringMessage())
            : new RomanNumber(_value - a.Value);

    public static RomanNumber Subtract(int num1, int num2) // overloading for two ints (static)
        => new RomanNumber(num1 - num2);

    public static RomanNumber Subtract(string num1, string num2) // overloading for two strings (static)
        => (num1 is null || num2 is null)
            ? throw new ArgumentNullException()
            : new RomanNumber(RomanNumber.Parse(num1)).Subtract(new RomanNumber(RomanNumber.Parse(num2)));

    public static RomanNumber Subtract(RomanNumber num1, RomanNumber num2) // overloading for two RomanNumbers (static)
        => (num1 is null || num2 is null)
            ? throw new ArgumentNullException()
            : num1.Subtract(num2);

    public static RomanNumber Subtract(RomanNumber num1, string num2) // overloading for RomanNumber and string (static)
        => (num1 is null || num2 is null)
            ? throw new ArgumentNullException()
            : new RomanNumber(num1.Subtract(new RomanNumber(RomanNumber.Parse(num2))));

    public static RomanNumber Subtract(string num1, RomanNumber num2) // overloading for string and RomanNumber (static)
        => (num1 is null || num2 is null)
            ? throw new ArgumentNullException()
            : new RomanNumber(new RomanNumber(RomanNumber.Parse(num1)).Subtract(num2));

    public static RomanNumber Subtract(RomanNumber num1, int num2) // overloading for RomanNumber and int (static)
        => (num1 is null)
            ? throw new ArgumentNullException()
            : new RomanNumber(num1.Subtract(new RomanNumber(num2)));

    public static RomanNumber Subtract(int num1, RomanNumber num2) // overloading for int and RomanNumber (static)
        => (num2 is null)
            ? throw new ArgumentNullException()
            : new RomanNumber(new RomanNumber(num1).Subtract(num2));

    public static RomanNumber Subtract(int num1, string num2) // overloading for int and string (static)
        => (num2 is null)
            ? throw new ArgumentNullException()
            : new RomanNumber(new RomanNumber(num1).Subtract(new RomanNumber(RomanNumber.Parse(num2))));


    public static RomanNumber Subtract(string num1, int num2) // overloading for string and int (static)
        => (num1 is null)
            ? throw new ArgumentNullException()
            : new RomanNumber(new RomanNumber(RomanNumber.Parse(num1)).Subtract(new RomanNumber(num2)));

    public static RomanNumber Mult(object obj1, object obj2) // fabrice method Mult for all types 
        => new RomanNumber(obj1).Mult(new RomanNumber(obj2));


    public RomanNumber Mult(int other) // Mult method overloading for int (not static)
        => new RomanNumber(Value * other);


    public RomanNumber Mult(string other) // Mult method overloading for string (not static)
        => (other == "-" && other.Length == 1)
            ? throw new ArgumentException(Resources.GetInvalidCharMessage('-'))
            : (string.IsNullOrEmpty(other))
                ? throw new ArgumentNullException(Resources.GetEmptyStringMessage())
                : new RomanNumber(_value * Parse(other));

    public RomanNumber Mult(RomanNumber a) // Mult method overloading for RomanNumber (not static)
        => (a is null)
            ? throw new ArgumentNullException(Resources.GetEmptyStringMessage())
            : new RomanNumber(_value * a.Value);

    public static RomanNumber Mult(int num1, int num2) // overloading for two ints (static)
        => new RomanNumber(num1 * num2);

    public static RomanNumber Mult(string num1, string num2) // overloading for two strings (static)
        => (num1 is null || num2 is null)
            ? throw new ArgumentNullException()
            : new RomanNumber(RomanNumber.Parse(num1)).Mult(new RomanNumber(RomanNumber.Parse(num2)));

    public static RomanNumber Mult(RomanNumber num1, RomanNumber num2) // overloading for two RomanNumbers (static)
        => (num1 is null || num2 is null)
            ? throw new ArgumentNullException()
            : num1.Mult(num2);

    public static RomanNumber Mult(RomanNumber num1, string num2) // overloading for RomanNumber and string (static)
        => (num1 is null || num2 is null)
            ? throw new ArgumentNullException()
            : new RomanNumber(num1.Mult(new RomanNumber(RomanNumber.Parse(num2))));

    public static RomanNumber Mult(string num1, RomanNumber num2) // overloading for string and RomanNumber (static)
        => (num1 is null || num2 is null)
            ? throw new ArgumentNullException()
            : new RomanNumber(new RomanNumber(RomanNumber.Parse(num1)).Mult(num2));

    public static RomanNumber Mult(RomanNumber num1, int num2) // overloading for RomanNumber and int (static)
        => (num1 is null)
            ? throw new ArgumentNullException()
            : new RomanNumber(num1.Mult(new RomanNumber(num2)));

    public static RomanNumber Mult(int num1, RomanNumber num2) // overloading for int and RomanNumber (static)
        => (num2 is null)
            ? throw new ArgumentNullException()
            : new RomanNumber(new RomanNumber(num1).Mult(num2));

    public static RomanNumber Mult(int num1, string num2) // overloading for int and string (static)
        => (num2 is null)
            ? throw new ArgumentNullException()
            : new RomanNumber(new RomanNumber(num1).Mult(new RomanNumber(RomanNumber.Parse(num2))));


    public static RomanNumber Mult(string num1, int num2) // overloading for string and int (static)
        => (num1 is null)
            ? throw new ArgumentNullException()
            : new RomanNumber(new RomanNumber(RomanNumber.Parse(num1)).Mult(new RomanNumber(num2)));


    public static RomanNumber Div(object obj1, object obj2) // fabrice method Div for all types 
        => new RomanNumber(obj1).Div(new RomanNumber(obj2));


    public RomanNumber Div(int other) // Div method overloading for int (not static)
        => (other == 0)
            ? throw new DivideByZeroException(Resources.GetDivideByZeroMessage())
            : new RomanNumber(Value / other);


    public RomanNumber Div(string other) // Div method overloading for string (not static)
        => (other == "-" && other.Length == 1)
            ? throw new ArgumentException(Resources.GetInvalidCharMessage('-'))
            : (string.IsNullOrEmpty(other))
                ? throw new ArgumentNullException(Resources.GetEmptyStringMessage())
                : (other == "0")
                    ? throw new DivideByZeroException(Resources.GetDivideByZeroMessage())
                    : new RomanNumber(_value / Parse(other));

    public RomanNumber Div(RomanNumber a) // Div method overloading for RomanNumber (not static)
        => (a is null)
            ? throw new ArgumentNullException(Resources.GetEmptyStringMessage())
            : (a.Value == 0)
                ? throw new DivideByZeroException(Resources.GetDivideByZeroMessage())
                : new RomanNumber(_value / a.Value);

    public static RomanNumber Div(int num1, int num2) // overloading for two ints (static)
        => (num2 == 0)
            ? throw new DivideByZeroException(Resources.GetDivideByZeroMessage())
            : new RomanNumber(num1 / num2);

    public static RomanNumber Div(string num1, string num2) // overloading for two strings (static)
        => (num1 is null || num2 is null)
            ? throw new ArgumentNullException()
            : (num2 == "0")
                ? throw new DivideByZeroException(Resources.GetDivideByZeroMessage())
                : new RomanNumber(RomanNumber.Parse(num1)).Div(new RomanNumber(RomanNumber.Parse(num2)));

    public static RomanNumber Div(RomanNumber num1, RomanNumber num2) // overloading for two RomanNumbers (static)
        => (num1 is null || num2 is null)
            ? throw new ArgumentNullException()
            : (num2.Value == 0)
                ? throw new DivideByZeroException(Resources.GetDivideByZeroMessage())
                : num1.Div(num2);

    public static RomanNumber Div(RomanNumber num1, string num2) // overloading for RomanNumber and string (static)
        => (num1 is null || num2 is null)
            ? throw new ArgumentNullException()
            : (num2 == "0")
                ? throw new DivideByZeroException(Resources.GetDivideByZeroMessage())
                : new RomanNumber(num1.Div(new RomanNumber(RomanNumber.Parse(num2))));

    public static RomanNumber Div(string num1, RomanNumber num2) // overloading for string and RomanNumber (static)
        => (num1 is null || num2 is null)
            ? throw new ArgumentNullException()
            : (num2.Value == 0)
                ? throw new DivideByZeroException(Resources.GetDivideByZeroMessage())
                : new RomanNumber(new RomanNumber(RomanNumber.Parse(num1)).Div(num2));

    public static RomanNumber Div(RomanNumber num1, int num2) // overloading for RomanNumber and int (static)
        => (num1 is null)
            ? throw new ArgumentNullException()
            : (num2 == 0)
                ? throw new DivideByZeroException(Resources.GetDivideByZeroMessage())
                : new RomanNumber(num1.Div(new RomanNumber(num2)));

    public static RomanNumber Div(int num1, RomanNumber num2) // overloading for int and RomanNumber (static)
        => (num2 is null)
            ? throw new ArgumentNullException()
            : (num2.Value == 0)
                ? throw new DivideByZeroException(Resources.GetDivideByZeroMessage())
                : new RomanNumber(new RomanNumber(num1).Div(num2));

    public static RomanNumber Div(int num1, string num2) // overloading for int and string (static)
        => (num2 is null)
            ? throw new ArgumentNullException()
            : (num2 == "0")
                ? throw new DivideByZeroException(Resources.GetDivideByZeroMessage())
                : new RomanNumber(new RomanNumber(num1).Div(new RomanNumber(RomanNumber.Parse(num2))));


    public static RomanNumber Div(string num1, int num2) // overloading for string and int (static)
        => (num1 is null)
            ? throw new ArgumentNullException()
            : (num2 == 0)
                ? throw new DivideByZeroException(Resources.GetDivideByZeroMessage())
                : new RomanNumber(new RomanNumber(RomanNumber.Parse(num1)).Div(new RomanNumber(num2)));
}