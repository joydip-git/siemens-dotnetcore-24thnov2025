using CalculationLibrary;

namespace CalculationExtensionLibrary
{
    public static class CalclationExtension
    {        
        public static int Subtract(this ICalculation calc, int a, int b)
        {
            var res = calc.Add(a, b);
            return a - b - res;
        }
    }
}
