using System;
namespace TodoListWeb.Models
{
	public class AddTodoItems
	{
        //public Guid Id { get; set; }

        public string Email { get; set; }

        public DateTime DueDate { get; set; }

        public string Title { get; set; }

        public bool IsDone { get; set; }

       
	}
}

