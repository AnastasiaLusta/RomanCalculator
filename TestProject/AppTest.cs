namespace RomanCalculator.App;

[TestClass]
public class AppTest
{
    [TestMethod]
    public void CalcTest()
    {
        //test if calc creates a new instance of the calculator
        Calc calc = new();
        Assert.IsNotNull(calc);
    }

    [TestMethod]
    public void RomanNumberParseTest()
    {
        //test for parsing any roman number
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
        // Check if RomanNumber.Parse throws ArgumentException with "N" in argument string with more letters
        var exc = Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("NM"));
        exc = Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("MN"));
        exc = Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("MNM"));

        // Check if RomanNumber.Parse throws ArgumentException with message "N is not allowed in context"
        var exp = new ArgumentException("N is not allowed in context");
        Assert.AreEqual(exc.Message, exp.Message);
    }

    
    [TestMethod]
    public void RomanNumberParse1Digit()
    {
        //test parser with 1 digit
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
        //test for invalid digits
        var exc = Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("A"));
        var exp = new ArgumentException("Invalid char A");
        Assert.AreEqual(exp.Message, exc.Message);
        var exc2 = Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("JJ"));
        var exp2 = new ArgumentException("Invalid char J");
        Assert.AreEqual(exp2.Message, exc2.Message);
        var exc3 = Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("QQQ"));
        var exp3 = new ArgumentException("Invalid char Q");
        Assert.AreEqual(exp3.Message, exc3.Message);
        
        //check if it starts with a specific message
        Assert.IsTrue(Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("X X")).
            Message.StartsWith("Invalid char"));
    }

    [TestMethod]
    public void RomanNumberParseEmpty()
    {
        //test for empty string
        var exc = Assert.ThrowsException<ArgumentNullException>(() => RomanNumber.Parse(""));
        
        //expected result and check if it equals to the actual result
        var exp = new ArgumentNullException("Empty string not allowed");
        Assert.AreEqual(exc.Message, exp.Message);

        // test if it throws an exception
        Assert.ThrowsException<ArgumentNullException>(() => RomanNumber.Parse(null!));
    }

    [TestMethod]
    public void RomanNumberParse2Digits()
    {
        //test parser with 2 digits
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
        //test parser with 3 or more digits
        Assert.AreEqual(30, RomanNumber.Parse("XXX"));
        Assert.AreEqual(401, RomanNumber.Parse("CDI"));
        Assert.AreEqual(1999, RomanNumber.Parse("MCMXCIX"));
    }
}