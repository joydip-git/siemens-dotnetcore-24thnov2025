using System.Net.Http.Json;
using System.Text.Json;

namespace AsynAwaitApp
{
    internal class Program
    {
        static int x = 0;
        static async Task Main(string[] args)
        {
            Console.WriteLine("Main method " + Environment.CurrentManagedThreadId);
            //try
            //{
            //    long sum = await Calculate();
            //    Console.WriteLine(sum);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //}

            GetDataAsync();
            Console.WriteLine("continue with main task...");
            GetDataAsync();
           
            await Task.Delay(1000);
        }
        static async Task GetDataAsync()
        {
            x++;
            Console.WriteLine("Task: " + x);
            var list = await FetchTodosAsync();
            if (list != null && list.Count > 0)
            {
                await WriteAsync(list);
                var todos = await ReadTodosAsync();
                todos?.ForEach(x => Console.WriteLine(x.Title));
            }
           
        }
        static async Task<long> Calculate()
        {
            Console.WriteLine("Calculate method " + Environment.CurrentManagedThreadId);
            long sum = 0;
            for (int i = 0; i < 1000000; i++)
            {
                sum += i;
            }
            return sum;
        }
        static async Task<List<Todo>?> FetchTodosAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                Task<List<Todo>?> todosTask = client.GetFromJsonAsync<List<Todo>>("https://jsonplaceholder.typicode.com/todos");
                Thread.Sleep(3000);
                return await todosTask;
            }
            catch (Exception)
            {
                throw;
            }
        }
        static async Task<List<Todo>?> ReadTodosAsync()
        {
            if (File.Exists("todos.json"))
            {
                using Stream stream = File.OpenRead("todos.json");
                List<Todo>? todos = await JsonSerializer.DeserializeAsync<List<Todo>?>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return todos;
            }
            else
                throw new FileNotFoundException("the file not found...");
        }
        static async Task WriteAsync(List<Todo> list)
        {
            if (File.Exists("todos.json"))
            {
                using Stream stream = File.OpenWrite("todos.json");
                await JsonSerializer.SerializeAsync<List<Todo>>(stream, list, new JsonSerializerOptions { WriteIndented = true });
            }
            else
                throw new FileNotFoundException("the file not found...");
        }
    }
}
