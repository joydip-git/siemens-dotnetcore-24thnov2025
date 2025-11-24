namespace DelegatedDemo
{
    //public delegate bool LogicDel(int number);
    public delegate bool LogicDel<in T>(T value);
    public delegate TResult LogicDel<in T, out TResult>(T value);
    public delegate TResult LogicDel<in T1, in T2, out TResult>(T1 value1, T2 value2);

    internal class Logic
    {
        public static bool IsEven(int num) => num % 2 == 0;
        public bool IsOdd(int num) => num % 2 != 0;
    }
}
