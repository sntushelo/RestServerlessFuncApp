using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using RestServerlessFuncApp.Core.Services;
using RestServerlessFuncApp.Core.Entities;
using Newtonsoft.Json;
using System.IO;

namespace RestServerlessFuncApp.Application.Functions
{
    public class UpdateTodo
    {
        readonly ITodosService _todosServices;

        public UpdateTodo(ITodosService todosServices)
        {
            _todosServices = todosServices;
        }

        [FunctionName("UpdateTodo")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "update", Route = "todos/{id}")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Todo>(requestBody);

            var updatedTodo = await _todosServices.UpdateTodoAsync(data);

            return new OkObjectResult(updatedTodo);
        }
    }
}
