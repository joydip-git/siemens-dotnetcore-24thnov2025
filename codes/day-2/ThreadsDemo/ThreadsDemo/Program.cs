namespace ThreadsDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine($"Main Thread Id: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Main Thread Id: {Environment.CurrentManagedThreadId}");
            Add(12, 13);

            //ThreadStart runDel = Run;
            ParameterizedThreadStart runDel = Run;
            Thread runThread = new Thread(runDel);
            runThread.Start(5);
            runThread.Join();
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Main: {i}");
                Thread.Sleep(1000);
            }

        }
        static void Run(object value)
        {
            Console.WriteLine($"Run Thread Id: {Environment.CurrentManagedThreadId}");
            for (int i = 0; i < (int)value; i++)
            {
                Console.WriteLine($"Run: {i}");
                Thread.Sleep(1000);
            }
        }

        private static void Add(int a, int b)
        {
            Console.WriteLine($"Add Thread Id: {Environment.CurrentManagedThreadId}");
            Console.WriteLine(a + b);
        }
    }
}
