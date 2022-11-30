using System;
using Microsoft.EntityFrameworkCore;
using TodoListWeb.Models;

namespace TodoListWeb.Data
{
	public class TodoAPIDbContext : DbContext
	{

        public TodoAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TodoItems> todoItems { get; set; }
    }
}

