namespace FirstCoreWebApp
{
    public class Repo : IRepo
    {
        public Repo()
        {
            Console.WriteLine("repo created...");
        }
        public string GetData() => "data";
    }
}
