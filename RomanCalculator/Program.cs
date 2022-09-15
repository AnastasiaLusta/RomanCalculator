using RomanCalculator.App;

RomanNumber.Resources = new Resources();

var calc = new Calc(RomanNumber.Resources); // DI for culture (language of UI)

calc.Run(); // runs the calculator of Roman numbers