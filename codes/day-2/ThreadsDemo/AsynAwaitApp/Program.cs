using System.Net.Http.Json;
using System.Text.Json;

namespace AsynAwaitApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Main method " + Environment.CurrentManagedThreadId);
            try
            {
                long sum = await Calculate();
                Console.WriteLine(sum);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //Task<List<Todo>?> todoTask = FetchTodos();
            //if (todoTask.IsFaulted)
            //    Console.WriteLine(todoTask.Exception);
            //todoTask.Result?.ForEach(td => Console.WriteLine(td.Title));
            //try
            //{
            //    Task<List<Todo>?> todoTask = FetchTodosAsync();
            //    List<Todo>? todos = await todoTask;
            //    todos?.ForEach(td => Console.WriteLine(td.Title));
            //    if (todos != null && todos.Count > 0)
            //        await WriteAsync(todos);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e); ;
            //}
            //Console.WriteLine("\n\n\n");
            //try
            //{
            //    List<Todo>? todos = await ReadTodosAsync();
            //    todos?.ForEach(td => Console.WriteLine(td.Title));
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //}
            //FetchTodosAsync()
            //    .ContinueWith(t =>
            //    {
            //        var list = t.Result;
            //        if(list != null && list.Count > 0)
            //        {
            //            WriteAsync(list).ContinueWith(
            //                t =>
            //                {
            //                    if (t.IsCompletedSuccessfully)
            //                    {
            //                        ReadTodosAsync()
            //                        .ContinueWith(t=>
            //                        {
            //                            if (t.IsCompletedSuccessfully)
            //                            {
            //                                t.Result?.ForEach(td => Console.WriteLine(td.Title));
            //                            }
            //                        }
            //                        );
            //                    }
            //                });
            //        }
            //    });

            try
            {
                var list = await FetchTodosAsync();
                if (list != null && list.Count > 0)
                {
                    await WriteAsync(list);
                    var todos = await ReadTodosAsync();
                    todos?.ForEach(x => Console.WriteLine(x.Title));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
        static async Task<long> Calculate()
        {
            Console.WriteLine("Calculate method " + Environment.CurrentManagedThreadId);
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
