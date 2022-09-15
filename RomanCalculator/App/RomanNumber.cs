namespace RomanCalculator.App;

public record RomanNumber
{
    private const char ZERO_DIGIT = 'N';
    private int _value;
    public static Resources Resources { get; set; } = null!;

    public int Value
    {
        get { return _value; }
        set { _value = value; }
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
        if (romanNumber == null || romanNumber == "")
        {
            throw new ArgumentNullException(Resources.GetEmptyStringMessage());
        }

        if (romanNumber == ZERO_DIGIT.ToString())
        {
            return 0;
        }

        // throw new ArgumentException of N is in the number with other letters
        if (romanNumber.Contains(ZERO_DIGIT) && romanNumber.Length > 1)
        {
            throw new ArgumentException(Resources.GetMispalcedNMessage());
        }

        //checks if it contains 
        foreach (var number in romanNumber)
        {
            if (!digits.ContainsKey(number))
            {
                if (number == '-')
                {
                    negative = true;
                    continue;
                }

                throw new ArgumentException(Resources.GetInvalidCharMessage((number)));
            }

            //get value of the number
            var current = digits[number];
            //check if the current number is bigger than the previous one
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

        //changes the result to negative if it contains -
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
            return ZERO_DIGIT.ToString();
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
    
    // overloading for RomanNumber object and int (not static)
    public static RomanNumber Add(object obj1, object obj2) // fabrice method Add for all types 
    {
        var rns = new RomanNumber[] { null!, null! };
        var pars = new object[] { obj1, obj2 };
        var res = new RomanNumber(0);
        for (int i = 0; i < 2; i++)
        {
            if (pars[i] is null)
                throw new ArgumentNullException(Resources.GetInvalidTypeMessage(i + 1, pars[i].GetType().Name));
            if (pars[i] is int val) rns[i] = new RomanNumber(val);
            else if (pars[i] is string str) rns[i] = new RomanNumber(Parse(str));
            else if (pars[i] is RomanNumber rn) rns[i] = rn;
            else throw new ArgumentException(Resources.GetInvalidTypeMessage(i + 1, pars[i].GetType().Name));

            res = res.Add(rns[i]);
        }

        return res;
    }
    
    public RomanNumber Add(int other) // Add method overloading for int (not static)
        => new RomanNumber(Value + other);


    public RomanNumber Add(string other) // Add method overloading for string (not static)
        => (other == "-" && other.Length == 1)
            ? throw new ArgumentException(Resources.GetMispalcedNMessage())
            : (string.Empty == other)
                ? throw new ArgumentException(Resources.GetEmptyStringMessage())
                : (other is null)
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
}