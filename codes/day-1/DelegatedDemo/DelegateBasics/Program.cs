namespace DelegateBasics
{
    delegate int CalcDel(int a, int b);
    internal class Program
    {
        static void Main(string[] args)
        {
            //anonymous method => method without a name but declared according to a delegate declaration and its referenced is immediately stored in that delegate
            CalcDel addDel = delegate (int a, int b)
            {
                return a + b;
            };

        }
    }
}
