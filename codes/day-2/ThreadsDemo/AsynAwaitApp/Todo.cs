namespace AsynAwaitApp
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int UserId { get; set; }
        public bool Completed { get; set; }
    }
}
