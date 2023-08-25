using RestServerlessFuncApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestServerlessFuncApp.Core.Services
{
    public interface ITodosService
    {
        Task<IList<Todo>> GetTodosAsync();
        Task<Todo> GetTodoAsync(Guid id);
        Task<Todo> AddTodoAsync(Todo todo);
        Task<Todo> UpdateTodoAsync(Todo todo);
        Task<Todo> DeleteTodoAsync(Guid id);
    }
}
