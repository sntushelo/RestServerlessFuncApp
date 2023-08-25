using Microsoft.EntityFrameworkCore;
using RestServerlessFuncApp.Core.Entities;
using RestServerlessFuncApp.Core.Services;
using RestServerlessFuncApp.Infra;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestServerlessFuncApp.Application.Services
{
    internal class TodosService : ITodosService
    {
        readonly AppDataContext _dataContext;

        public TodosService(AppDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Todo> AddTodoAsync(Todo todo)
        {
            var newTodo = new Todo
            {
                Name = todo.Name,
                Description = todo.Description,
                IsCompleted = todo.IsCompleted,
            };

            var createdItem = await _dataContext.Todos.AddAsync(newTodo);
            await _dataContext.SaveChangesAsync();
            
            return createdItem.Entity;
        }

        public async Task<Todo> DeleteTodoAsync(Guid id)
        {
            var todo = await _dataContext.Todos.FirstOrDefaultAsync(t => t.Id == id);
            
            if (todo == null)
                return null;

            _dataContext.Todos.Remove(todo);
            await _dataContext.SaveChangesAsync();

            return todo;
        }

        public async Task<Todo> GetTodoAsync(Guid id)
        {
            return await _dataContext.Todos.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IList<Todo>> GetTodosAsync()
        {
            return await _dataContext.Todos.ToListAsync();
        }

        public async Task<Todo> UpdateTodoAsync(Todo todo)
        {
            var existingTodo = await _dataContext.Todos.FirstOrDefaultAsync(t => t.Id == todo.Id);

            if (existingTodo == null)
                return null;

            existingTodo.Name = todo.Name;
            existingTodo.Description = todo.Description;
            existingTodo.IsCompleted = todo.IsCompleted;

            await _dataContext.SaveChangesAsync();
            return existingTodo;
        }
    }
}
