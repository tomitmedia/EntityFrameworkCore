namespace TodoListWeb.Models
{
    public class TodoResponse
    {
        public List<TodoItems> todoItems { get; set; } = new List<TodoItems>();

        public int Pages { get; set; }

        public int CurrentPage { get; set; }
    }
}
