namespace DelegatedDemo
{
    internal class Program
    {
        //static List<int> Filter(List<int> input, LogicDel funcRef)
        //{
        //    List<int> result = [];
        //    foreach (int item in input)
        //    {
        //        //if (item % 2 == 0)
        //        if (funcRef(item))
        //            result.Add(item);
        //    }
        //    return result;
        //}

        //static List<int> Filter(List<int> input, LogicDel<int> funcRef)
        //{
        //    List<int> result = [];
        //    foreach (int item in input)
        //    {
        //        //if (item % 2 == 0)
        //        if (funcRef(item))
        //            result.Add(item);
        //    }
        //    return result;
        //}

        //static List<T> Filter<T>(List<T> input, LogicDel<T> funcRef)
        //{
        //    List<T> result = [];
        //    foreach (T item in input)
        //    {
        //        //if (item % 2 == 0)
        //        if (funcRef(item))
        //            result.Add(item);
        //    }
        //    return result;
        //}
        static List<T> Filter<T>(List<T> input, Predicate<T> funcRef)
        {
            List<T> result = [];
            foreach (T item in input)
            {
                //if (item % 2 == 0)
                if (funcRef(item))
                    result.Add(item);
            }
            return result;
        }
        static void Main(string[] args)
        {
            //source of data
            List<int> numbers = [1, 4, 3, 6, 2, 7, 5, 0, 8, 9];

            //LogicDel<int> evenDel = new LogicDel<int>(Logic.IsEven);
            Predicate<int> evenDel = new Predicate<int>(Logic.IsEven);
            List<int> evenNumbers = Filter<int>(numbers, evenDel);
            foreach (var item in evenNumbers)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\n\n");
            Logic logic = new();
            //LogicDel<int> oddDel = logic.IsOdd;
            Predicate<int> oddDel = logic.IsOdd;
            List<int> oddNumbers = Filter(numbers, oddDel);
            foreach (var item in oddNumbers)
            {
                Console.WriteLine(item);
            }

            //LogicDel isGreaterDel = delegate (int x)
            //{
            //    return x > 5;
            //};


            //Lambda expression: short hand mathetical expression of the logic written using anonymous method
            //LogicDel isGreaterDel = (int x) => x > 5;

            //type inference
            //LogicDel<int> isGreaterDel = (x) => x > 5;
            Predicate<int> isGreaterDel = (x) => x > 5;

            //var y = 10;
            //y = 12.34;
            Console.WriteLine("\n\n");
            List<int> greaterNumbers = Filter(numbers, isGreaterDel);
            foreach (var item in greaterNumbers)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\n\n");

            List<string> names = ["Arun", "Joy", "asif", "Deepak"];

            //LogicDel<string> startsWithALogic = name => name.ToLower().StartsWith('a');
            Predicate<string> startsWithALogic = name => name.ToLower().StartsWith('a');

            List<string> namesStaringWithA = Filter(names, startsWithALogic);

            foreach (var item in namesStaringWithA)
            {
                Console.WriteLine(item);
            }

            var obj = new { FirstName = "joydip", LastName = "mondal" };

        }
        //static unsafe void Method()
        //{
        //    int a = 10;
        //    int* p = (int*)&a;
        //}
    }
}
