using Microsoft.EntityFrameworkCore;
using System;
using WebAPI.Controllers;
using WebAPI.Models;
using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAPITests
{
    public class TodoItemsTests
    {        

        [Fact]
        public void Deve_Inserir_Item()
        {
            //TodoItemsController todo = new TodoItemsController()
            var options = new DbContextOptionsBuilder<TodoContext>().UseInMemoryDatabase(databaseName: "DeveInserirItem").Options;

            using (var context = new TodoContext(options))
            {
                var todoItems = new TodoItemsController(context);
                
                var todoInserted = InsertTodoItem(context);
                var todoGeted = GetToDoItem(context, todoInserted.Id);

                Assert.Equal(todoGeted, todoInserted);
            }
        }

        public TodoItem GetToDoItem(TodoContext context, long id)
        {
            var todoItems = new TodoItemsController(context);
            return todoItems.GetTodoItem(id).Result.Value;
        }

        public TodoItem InsertTodoItem(TodoContext context)
        {
            var todoItems = new TodoItemsController(context);
            var todo = new TodoItem() { Name = "Lavar louça", IsComplete = false };
            var postReturn = todoItems.PostTodoItem(todo);

            return ((TodoItem)((ObjectResult)postReturn.Result.Result).Value);
        }
    }
}
