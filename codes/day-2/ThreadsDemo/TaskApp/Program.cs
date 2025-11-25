namespace TaskApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            try
            {
                Console.WriteLine($"Main: {Environment.CurrentManagedThreadId}");
                Task<long> calcTask = Calculate();
                calcTask
                    .ContinueWith(t =>
                    {

                        if (t.IsFaulted)
                        {
                            Console.WriteLine(t.Exception.InnerException?.ToString());
                        }
                        else
                        {
                            Console.WriteLine($"CW: {Environment.CurrentManagedThreadId}");
                            Console.WriteLine(t.Result);
                            Console.WriteLine("task completed finally...");
                        }

                    });
                Console.WriteLine("main task continues");
                Console.WriteLine("hello...");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            */
            Console.WriteLine($"Main: {Environment.CurrentManagedThreadId}");
            Task<long> calcTask = Calculate();
            
            Console.WriteLine(calcTask.Result);
            Console.WriteLine("Main over...");
        }
        static Task<long> Calculate()
        {
            Console.WriteLine($"calculate method: {Environment.CurrentManagedThreadId}");
            Func<long> func = () =>
            {
                Console.WriteLine($"calculating: {Environment.CurrentManagedThreadId}");
                long sum = 0;
                for (int i = 0; i < 1000000; i++)
                {
                    sum += i;
                    //if (sum > 5000)
                    //{
                    //    throw new Exception("value exceded");
                    //}
                }
                return sum;
            };

            Task<long> task = new Task<long>(func);
            task.Start();
            //Task<long> task = Task<long>.Run(func);
            return task;
        }
    }
}
