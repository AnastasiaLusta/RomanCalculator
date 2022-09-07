namespace RomanCalculator.App;

[TestClass]
public class AppTest
{
    [TestMethod]
    public void CalcTest()
    {
        Calc calc = new();
        Assert.IsNotNull(calc);
    }

    [TestMethod]
    public void RomanNumberParseTest()
    {
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
        Assert.AreEqual(0, RomanNumber.Parse("N"));
        // проверка на N
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
}