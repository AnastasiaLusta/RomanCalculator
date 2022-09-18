namespace RomanCalculator.App;

[TestClass]
public class AppTest
{
    private Resources Resources { get; set; } = new();
    public AppTest()
    {
        RomanNumber.Resources = Resources;
    }
    [TestMethod]
    public void CalcTest()
    {
        // test if calc creates a new instance of the calculator
        Calc calc = new(Resources);
        Assert.IsNotNull(calc);
    }
    
    [TestMethod]
    public void EvalExpressionTest()
    {
        Calc calc = new(Resources);
        Assert.IsNotNull(calc.EvalExpression("XI + IV"));
        Assert.AreEqual(new RomanNumber(10), calc.EvalExpression("XI - I"));
        Assert.ThrowsException<ArgumentException>(() => calc.EvalExpression("2 + 3"));
    }

    [TestMethod]
    public void RomanNUmberCtorTest()
    {
        RomanNumber romanNumber = new();
        Assert.IsNotNull(romanNumber);
        romanNumber = new(10);
        Assert.IsNotNull(romanNumber);
        romanNumber = new();
        Assert.IsNotNull(romanNumber);
    }

    [TestMethod]
    public void RomanNumberParseTest()
    {
        // test for parsing any roman number
        Assert.AreEqual(RomanNumber.Parse("I"), 1, "I == 1");
        Assert.AreEqual(RomanNumber.Parse("IV"), 4, "IV == 4");
        Assert.AreEqual(RomanNumber.Parse("XV"), 15);
        Assert.AreEqual(RomanNumber.Parse("XXX"), 30);
        Assert.AreEqual(RomanNumber.Parse("CM"), 900);
        Assert.AreEqual(RomanNumber.Parse("MCMXCIX"), 1999);
        Assert.AreEqual(RomanNumber.Parse("CD"), 400);
        Assert.AreEqual(RomanNumber.Parse("CDI"), 401);
        Assert.AreEqual(RomanNumber.Parse("LV"), 55);
        Assert.AreEqual(RomanNumber.Parse("XL"), 40);
    }

    [TestMethod]
    public void RomanNumberParseAllowN()
    {
        //check if it returns 0 when N is found
        Assert.AreEqual(0, RomanNumber.Parse("N"));
    }

    [TestMethod]
    public void RomanNumberParseDisallowN()
    {
        // check if RomanNumber.Parse throws ArgumentException with "N" in argument string with more letters
        var exc = Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("NM"));

        // check if RomanNumber.Parse throws ArgumentException with message "N is not allowed in context"
        var exp = new ArgumentException(Resources.GetMispalcedNMessage());
        Assert.AreEqual(exc.Message, exp.Message);
    }

    [TestMethod]
    public void RomanNumberToString()
    {
        RomanNumber romanNumber = new();
        Assert.AreEqual("N", romanNumber.ToString());

        romanNumber = new(10);
        Assert.AreEqual("X", romanNumber.ToString());

        romanNumber = new(90);
        Assert.AreEqual("XC", romanNumber.ToString());

        romanNumber = new(20);
        Assert.AreEqual("XX", romanNumber.ToString());

        romanNumber = new(1999);
        Assert.AreEqual("MCMXCIX", romanNumber.ToString());
    }

    [TestMethod]
    public void RomanNumberParserCrossTest()
    {
        // test if the parser and the toString method are working together
        RomanNumber romanNumber = new();
        for (int n = 1; n < 2022; n++)
        {
            romanNumber.Value = n;
            Assert.AreEqual(n, RomanNumber.Parse(romanNumber.ToString()));
        }
    }

    public void RomanNumberParse1Digit()
    {
        // test parser with 1 digit
        Assert.AreEqual(1, RomanNumber.Parse("I"));
        Assert.AreEqual(5, RomanNumber.Parse("V"));
        Assert.AreEqual(10, RomanNumber.Parse("X"));
        Assert.AreEqual(50, RomanNumber.Parse("L"));
        Assert.AreEqual(100, RomanNumber.Parse("C"));
        Assert.AreEqual(500, RomanNumber.Parse("D"));
        Assert.AreEqual(1000, RomanNumber.Parse("M"));
    }

    [TestMethod]
    public void RomanNumberInvalidDigit()
    {
        // test for invalid digits
        var exc = Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("A"));
        var exp = new ArgumentException(Resources.GetInvalidCharMessage('A'));
        Assert.AreEqual(exp.Message, exc.Message);
        var exc2 = Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("JJ"));
        var exp2 = new ArgumentException(Resources.GetInvalidCharMessage('J'));
        Assert.AreEqual(exp2.Message, exc2.Message);
        var exc3 = Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("QQQ"));
        var exp3 = new ArgumentException(Resources.GetInvalidCharMessage('Q'));
        Assert.AreEqual(exp3.Message, exc3.Message);

        // check if it starts with a specific message
        Assert.IsTrue(Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("1X X1")).Message
            .StartsWith(Resources.GetInvalidCharMessage('1')));
    }

    [TestMethod]
    public void RomanNumberParseEmpty()
    {
        // test for empty string
        var exc = Assert.ThrowsException<ArgumentNullException>(() => RomanNumber.Parse(""));

        // expected result and check if it equals to the actual result
        var exp = new ArgumentNullException(Resources.GetEmptyStringMessage());
        Assert.AreEqual(exc.Message, exp.Message);

        // test if it throws an exception
        Assert.ThrowsException<ArgumentNullException>(() => RomanNumber.Parse(null!));
    }

    [TestMethod]
    public void RomanNumberParse2Digits()
    {
        // test parser with 2 digits
        Assert.AreEqual(4, RomanNumber.Parse("IV"));
        Assert.AreEqual(15, RomanNumber.Parse("XV"));
        Assert.AreEqual(900, RomanNumber.Parse("CM"));
        Assert.AreEqual(400, RomanNumber.Parse("CD"));
        Assert.AreEqual(55, RomanNumber.Parse("LV"));
        Assert.AreEqual(40, RomanNumber.Parse("XL"));
    }


    [TestMethod]
    public void RomanNumberParse3MoreDigits()
    {
        // test parser with 3 or more digits
        Assert.AreEqual(30, RomanNumber.Parse("XXX"));
        Assert.AreEqual(401, RomanNumber.Parse("CDI"));
        Assert.AreEqual(1999, RomanNumber.Parse("MCMXCIX"));
    }
    
    [TestMethod]
    public void RomanNumberTypeTest()
    {
       
        RomanNumber rn1 = new() { Value = 10 };
        RomanNumber rn2 = rn1;
        Assert.AreSame(rn1, rn2);       
            
        RomanNumber rn3 = rn1 with { };  
        Assert.AreNotSame(rn3, rn1);  
        Assert.AreEqual(rn3, rn1);       
        Assert.IsTrue(rn1 == rn3);      

        RomanNumber rn4 = rn1 with { Value = 20 };
        
        Assert.AreNotEqual(rn4, rn1);    
        Assert.IsFalse(rn1 == rn4);      
        Assert.IsTrue(rn1 != rn4);      

    }

    [TestMethod]
    public void RomanNumberNegativeParsing()
    {
        Assert.AreEqual(-10, RomanNumber.Parse("-X"));
        Assert.AreEqual(-1999, RomanNumber.Parse("-MCMXCIX"));
        Assert.AreEqual(-900, RomanNumber.Parse("-CM"));
        Assert.AreEqual(-400, RomanNumber.Parse("-CD"));
        // testing of negative parsing
    }

    [TestMethod]
    public void RomanNumberNegativeToString()
    {
        // test negative number
        RomanNumber romanNumber = new RomanNumber();
        Assert.AreEqual("N", romanNumber.ToString());

        romanNumber = new RomanNumber(-10);
        Assert.AreEqual("-X", romanNumber.ToString());

        romanNumber = new RomanNumber(-90);
        Assert.AreEqual("-XC", romanNumber.ToString());

        romanNumber = new RomanNumber(-20);
        Assert.AreEqual("-XX", romanNumber.ToString());

        romanNumber = new RomanNumber(-1999);
        Assert.AreEqual("-MCMXCIX", romanNumber.ToString());
    }
}

[TestClass]
public class RomanNumberOperationTest
{
    private Resources Resources { get; set; } = new();
    public RomanNumberOperationTest()
    {
        RomanNumber.Resources = Resources;
    }
    [TestMethod]
    public void RomanNumberAddTest()
    {
        // test addition for different combinations of types (RomanNumber, int, string)
        Assert.AreEqual(10, RomanNumber.Add("V", "V").Value);
        Assert.AreEqual(20, RomanNumber.Add("X", "X").Value);
        Assert.AreEqual(30, RomanNumber.Add("XX", "X").Value);
        Assert.AreEqual(40, RomanNumber.Add("XX", "XX").Value);
        Assert.AreEqual(50, RomanNumber.Add("XX", "XXX").Value);
        Assert.AreEqual(0, RomanNumber.Add("-XXX", "XXX").Value);
        Assert.AreEqual(0, RomanNumber.Add("XXX", "-XXX").Value);
        Assert.AreEqual(-60, RomanNumber.Add("-XXX", "-XXX").Value);
        Assert.AreEqual(60, RomanNumber.Add("XXX", "XXX").Value);
        Assert.AreEqual("III", RomanNumber.Add("I", "II").ToString());

        Assert.AreEqual(20, RomanNumber.Add(10,10).Value);
        Assert.AreEqual(100, RomanNumber.Add(90,10).Value);
        
        Assert.AreEqual(20, RomanNumber.Add(10,10).Value);

       
        // check for exceptions using different combinations of types (RomanNumber, int, string)
        // check how it behaves if there mistakes in strings or nulls
        Assert.ThrowsException<ArgumentException>(() => RomanNumber.Add("X", "X X"));
        Assert.ThrowsException<ArgumentException>(() => RomanNumber.Add("X", "Xx"));
        Assert.ThrowsException<ArgumentNullException>(()=> RomanNumber.Add((string)null!, "X"));
        Assert.ThrowsException<ArgumentNullException>(()=> RomanNumber.Add("X", (string)null!));
        Assert.ThrowsException<ArgumentNullException>(()=> RomanNumber.Add((RomanNumber)null!, "X"));
        Assert.ThrowsException<ArgumentNullException>(()=> RomanNumber.Add("X", (RomanNumber)null!));
        Assert.ThrowsException<ArgumentNullException>(()=> RomanNumber.Add((string)null!, (string)null!));
        Assert.ThrowsException<ArgumentNullException>(()=> RomanNumber.Add((string)null!, (RomanNumber)null!));
        Assert.ThrowsException<ArgumentNullException>(()=> RomanNumber.Add((RomanNumber)null!, (string)null!));

    }

    [TestMethod]
    public void AddTest()
    {
        // test addition for different combinations of types (RomanNumber, int, string)
        var rn = new RomanNumber(10);
        Assert.AreEqual(20,    rn.Add(10).Value);
        Assert.AreEqual(30,    rn.Add("XX").Value);
        Assert.AreEqual("V",   rn.Add(-5).ToString());
        Assert.AreEqual("-XL", rn.Add("-L").ToString());
        // check for exceptions using different combinations of types (RomanNumber, int, string) and nulls
        Assert.ThrowsException<ArgumentNullException>(()=> rn.Add(""));
        Assert.ThrowsException<ArgumentException>(() => rn.Add("-"));
        Assert.ThrowsException<ArgumentException>(() => rn.Add("10"));
        Assert.ThrowsException<ArgumentNullException>(() => rn.Add((String)null!));
        Assert.ThrowsException<ArgumentNullException>(() => rn.Add((RomanNumber)null!));
    }

    [TestMethod]
    public void SubstractTest()
    {
        // test subtraction for different combinations of types (RomanNumber, int, string)
        var rn = new RomanNumber(100);
        Assert.AreEqual(5, rn.Subtract(95).Value);
        Assert.AreEqual(90, rn.Subtract("X").Value);
        Assert.AreEqual("CL", rn.Subtract(-50).ToString());
        Assert.AreEqual("CL", rn.Subtract("-L").ToString());
        // check for exceptions using different combinations of types (RomanNumber, int, string) and nulls
        Assert.ThrowsException<ArgumentNullException>(()=> rn.Subtract(""));
        Assert.ThrowsException<ArgumentException>(() => rn.Subtract("-"));
        Assert.ThrowsException<ArgumentException>(() => rn.Subtract("10"));
        Assert.ThrowsException<ArgumentNullException>(() => rn.Subtract((String)null!));
        Assert.ThrowsException<ArgumentNullException>(() => rn.Subtract((RomanNumber)null!));
    }

    [TestMethod]
    public void MultiplicationTest()
    {
        // test multiplication for different combinations of types (RomanNumber, int, string)
        var rn = new RomanNumber(10);
        Assert.AreEqual(100, rn.Mult(10).Value);
        Assert.AreEqual(100, rn.Mult("X").Value);
        Assert.AreEqual("-C", rn.Mult(-10).ToString());
        Assert.AreEqual("-C", rn.Mult("-X").ToString());
        // check for exceptions using different combinations of types (RomanNumber, int, string) and nulls
        Assert.ThrowsException<ArgumentNullException>(() => rn.Mult(""));
        Assert.ThrowsException<ArgumentException>(() => rn.Mult("-"));
        Assert.ThrowsException<ArgumentException>(() => rn.Mult("10"));
        Assert.ThrowsException<ArgumentNullException>(() => rn.Mult((String)null!));
        Assert.ThrowsException<ArgumentNullException>(() => rn.Mult((RomanNumber)null!));
    }

    [TestMethod]
    public void DivisionTest()
    {
        // test division for different combinations of types (RomanNumber, int, string)
        var rn = new RomanNumber(100);
        Assert.AreEqual(10, rn.Div(10).Value);
        Assert.AreEqual(10, rn.Div("X").Value);
        Assert.AreEqual("-X", rn.Div(-10).ToString());
        Assert.AreEqual("-X", rn.Div("-X").ToString());
        // check for zero division
        Assert.ThrowsException<DivideByZeroException>(() => rn.Div(0));
        Assert.ThrowsException<DivideByZeroException>(() => rn.Div("0"));
        Assert.ThrowsException<DivideByZeroException>(() => rn.Div("N"));
        // check for exceptions using different combinations of types (RomanNumber, int, string) and nulls
        Assert.ThrowsException<ArgumentNullException>(() => rn.Div(""));
        Assert.ThrowsException<ArgumentException>(() => rn.Div("-"));
        Assert.ThrowsException<ArgumentException>(() => rn.Div("10"));
        Assert.ThrowsException<ArgumentNullException>(() => rn.Div((String)null!));
        Assert.ThrowsException<ArgumentNullException>(() => rn.Div((RomanNumber)null!));
    }

   
}
