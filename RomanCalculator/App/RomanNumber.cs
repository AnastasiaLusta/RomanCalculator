namespace RomanCalculator.App;

public record RomanNumber
{
    private int _value;
    public int Value
    {
        get { return _value; }
        set
        {
            _value = value;
        }
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
        var result = 0;
        var previous = 0;
        if (romanNumber == null || romanNumber == "")
        {
            throw new ArgumentNullException("Empty string not allowed");
        }
        
        if (romanNumber == "N")
        {
            return 0;
        }
        
        // throw new ArgumentException of N is in the number with other letters
        if (romanNumber.Contains("N") && romanNumber.Length > 1)
        {
            throw new ArgumentException("N is not allowed");
        }
        
        
        foreach (var number in romanNumber)
        {
            if (!digits.ContainsKey(number))
            {
                throw new ArgumentException($"Invalid char {number}");
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

        return result;
    }

        public override string ToString()
        {
            if (this._value == 0)
            {
                return "N";
            }

            var num = this._value;
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
}