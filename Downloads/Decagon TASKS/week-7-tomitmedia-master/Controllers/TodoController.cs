using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListWeb.Data;
using TodoListWeb.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoListWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly TodoAPIDbContext todoAPIDbContext;

        public TodoController(TodoAPIDbContext todoAPIDbContext)
        {
            this.todoAPIDbContext = todoAPIDbContext;
        }

        // GET: /<controller>/
        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {
            return Ok(await todoAPIDbContext.todoItems.ToListAsync());
        }

        [HttpGet("{pages}")]
        public async Task<ActionResult<List<TodoItems>>> GetPages(int pages)
        {
            if (todoAPIDbContext.todoItems == null)
            {
                return NotFound();
            }
            var pageResult = 2f;
            var pagecount = Math.Ceiling(todoAPIDbContext.todoItems.Count() / pageResult);
            var todo = await todoAPIDbContext.todoItems
                .Skip((pages - 1) * (int)pageResult)
                .Take((int)pageResult).ToListAsync();
            return Ok(todo);

        }


        //[HttpGet]
        //[Route("{id:guid}")]

        //public async Task<IActionResult> GetTodoItem([FromRoute] Guid id)
        //{
        //    var todoItems = await todoAPIDbContext.todoItems.FindAsync(id);

        //    if (todoItems == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(todoItems);
        //}

        [HttpGet("(API/Seach Todo)")]
        public async Task<IActionResult> SearchTodoItem( Guid id, string email, DateTime date)
        {
            var todoItems = await todoAPIDbContext.todoItems.FindAsync(id);
            var todoItems1 = await todoAPIDbContext.todoItems.FindAsync(email);
            var todoItems2 = await todoAPIDbContext.todoItems.FindAsync(date);

            if (todoItems == null|| todoItems1 == null || todoItems2 == null)
            {
                return NotFound();
            }
            return Ok(todoItems);
        }



        [HttpPost]
        public async Task<IActionResult> AddTodoItems(AddTodoItems addTodoItems)
        {
            var todoItems = new TodoItems()
            {
                Id = Guid.NewGuid(),
                AddDate = DateTime.Now,
                Email = addTodoItems.Email,
                DueDate = addTodoItems.DueDate,
                Title = addTodoItems.Title,
                IsDone = addTodoItems.IsDone

            };

            await todoAPIDbContext.todoItems.AddAsync(todoItems);
            await todoAPIDbContext.SaveChangesAsync();

            return Ok(todoItems);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateTodoItems([FromRoute] Guid id, UpdateTodoItems updateTodoItems)
        {
            var todoItems = todoAPIDbContext.todoItems.Find(id);

            if (todoItems != null)
            {
                todoItems.Email = updateTodoItems.Email;
                todoItems.DueDate = updateTodoItems.DueDate;
                todoItems.Title = updateTodoItems.Title;
                todoItems.IsDone = updateTodoItems.IsDone;

                await todoAPIDbContext.SaveChangesAsync();

                return Ok(todoItems);
            }

            return NotFound();

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteTodoItems([FromRoute] Guid id)
        {
            var todoItems = await todoAPIDbContext.todoItems.FindAsync(id);

            if (todoItems != null)
            {
                todoAPIDbContext.Remove(todoItems);
                await todoAPIDbContext.SaveChangesAsync();
                return Ok(todoItems);
            }

            return NotFound();
        }

       
    }

}

