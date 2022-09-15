namespace RomanCalculator.App;

public class Calc
{
   private readonly Resources Resources;  // DI

   public Calc(Resources resources)
      => Resources = resources;

   public void Run()
   {
      // RomanNumber rn = null!;
      // do
      // {
      //    try
      //    {
      //       Console.WriteLine(Resources.GetEnterNumberMessage());
      //       rn = new RomanNumber(RomanNumber.Parse(Console.ReadLine()!));
      //    }
      //    catch
      //    {
      //       Console.WriteLine("Error");
      //       continue;
      //    }
      // } while (rn == null);
      //
      // RomanNumber rn2 = null!;
      // do
      // {
      //    try
      //    {
      //       Console.WriteLine(Resources.GetEnterNumberMessage());
      //       rn2 = new RomanNumber(RomanNumber.Parse(Console.ReadLine()!));
      //    }
      //    catch
      //    {
      //       Console.WriteLine("Error");
      //       continue;
      //    }
      // } while (rn2 == null);
      // Console.WriteLine(RomanNumber.Add(rn,rn2));
   }
}