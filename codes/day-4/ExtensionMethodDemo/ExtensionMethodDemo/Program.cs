// See https://aka.ms/new-console-template for more information
using CalculationLibrary;
using CalculationExtensionLibrary;

Calculation calc = new Calculation();
int result = calc.Add(5, 10);
Console.WriteLine($"The result of addition is: {result}");

int subtractResult = calc.Subtract(10, 5);
Console.WriteLine($"The result of subtraction is: {subtractResult}");

List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
numbers.Filter(n => n % 2 == 0);

HashSet<int> set = new HashSet<int> { 1, 2, 3, 4, 5 };

//query syntax
var query = from num in numbers
            where num % 2 == 0
            orderby num descending
            select num;

//method syntax
numbers
    .Where(num=>num%2==0)
    .OrderByDescending(num=>num);