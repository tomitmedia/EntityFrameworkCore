using System;
namespace TodoListWeb.Models
{
	public class TodoItems
	{
		public Guid Id { get; set; }

		public string  Email{ get; set; }
        public DateTime AddDate { get; set; }

		public DateTime DueDate { get; set; }

        public string Title { get; set; }

		public bool IsDone { get; set; }

		
	}
}

