using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using RestServerlessFuncApp.Core.Services;

namespace RestServerlessFuncApp.Application.Functions
{
    public class GetTodo
    {
        readonly ITodosService _todosServices;
        public GetTodo(ITodosService todosServices)
        {
            _todosServices = todosServices;
        }

        [FunctionName("GetTodo")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "todos/{id}")] HttpRequest req, Guid id)
        {
            var todo = await _todosServices.GetTodoAsync(id);

            return new OkObjectResult(todo);
        }
    }
}
