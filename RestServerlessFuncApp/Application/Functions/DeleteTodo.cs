using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using RestServerlessFuncApp.Core.Services;

namespace RestServerlessFuncApp.Application.Functions
{
    public class DeleteTodo
    {
        readonly ITodosService _todosServices;

        public DeleteTodo(ITodosService todosServices)
        {
            _todosServices = todosServices;
        }

        [FunctionName("DeleteTodo")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "todos/{id}")] HttpRequest req, Guid id)
        {
            var todo = await _todosServices.DeleteTodoAsync(id);

            return new OkObjectResult(todo);
        }
    }
}
