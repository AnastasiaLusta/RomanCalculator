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
        var negative = false;
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
        
        //changes the result to negative if it contains -
        if (negative)
        {
            result = result * -1;
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
            
            if (num<0)
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
        //overloading for string and string
        public static RomanNumber Add(string a, string b)
        {
            if (a is null || b is null)
            {
                throw new ArgumentNullException("Empty string not allowed");
            }
            var result = Parse(a) + Parse(b);
            return new RomanNumber(result);
        }
        //overloading for int and int
        public static RomanNumber Add(int a, int b)
        {
            return new RomanNumber(a + b);
        }
        //overloading for RomanNumber and RomanNumber objects
        public static RomanNumber Add(RomanNumber a, RomanNumber b)
        {
            if (a is null || b is null)
            {
                throw new ArgumentNullException("Empty string not allowed");
            }
            return new RomanNumber(a.Value + b.Value);
        }
        //overloading for RomanNumber object and string
        public static RomanNumber Add(RomanNumber a, string b)
        {
            if (a is null || b is null)
            {
                throw new ArgumentNullException("Empty string not allowed");
            }
            return new RomanNumber(a.Value + Parse(b));
        }
        //overloading for string and RomanNumber object
        public static RomanNumber Add(string a, RomanNumber b)
        {
            if (a is null || b is null)
            {
                throw new ArgumentNullException("Empty string not allowed");
            }
            
            return new RomanNumber(Parse(a) + b.Value);
        }
        //overloading for RomanNumber object and int
        public static RomanNumber Add(RomanNumber a, int b)
        {
            if (a is null)
            {
                throw new ArgumentNullException("Empty string not allowed");
            }
            return new RomanNumber(a.Value + b);
        }
        //overloading for int and RomanNumber object
        public static RomanNumber Add(int a, RomanNumber b)
        {
            if (b is null)
            {
                throw new ArgumentNullException("Empty string not allowed");
            }
            return new RomanNumber(a + b.Value);
        }
        //overloading for string and int
        public static RomanNumber Add(string a, int b)
        {
            if (a is null)
            {
                throw new ArgumentNullException("Empty string not allowed");
            }
            return new RomanNumber(Parse(a) + b);
        }
        //overloading for int and string
        public static RomanNumber Add(int a, string b)
        {
            if (b is null)
            {
                throw new ArgumentNullException("Empty string not allowed");
            }
            return new RomanNumber(a + Parse(b));
        }
        //overloading for RomanNumber object and int (not static)
        public RomanNumber Add(int other)
        {
            return new RomanNumber(this._value + other);
        }
        
        //overloading for RomanNumber object and string (not static)
        public RomanNumber Add(string other)
        {
            if (other == "-" && other.Length == 1)
            {
                throw new ArgumentException("N is not allowed");
            }
            if (String.Empty == other)
            {
                throw new ArgumentException("Empty string not allowed");
            }
            if (other is null)
            {
                throw new ArgumentNullException("Empty string not allowed");
            }
            return new RomanNumber(this._value + Parse(other));
        }
        //overloading for RomanNumber object and RomanNumber object (not static)
        public RomanNumber Add(RomanNumber a)
        {
            if (a is null)
            {
                throw new ArgumentNullException("Empty string not allowed");
            }
            return new RomanNumber(this._value + a.Value);
        }
}