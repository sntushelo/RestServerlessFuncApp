using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestServerlessFuncApp.Core.Services;
using RestServerlessFuncApp.Core.Entities;

namespace RestServerlessFuncApp.Application.Functions
{
    public class AddTodo
    {
        readonly ITodosService _todosServices;

        public AddTodo(ITodosService todosServices)
        {
            _todosServices = todosServices;
        }

        [FunctionName("AddTodo")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "todos/")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Todo>(requestBody);

            var createdTodo = await _todosServices.AddTodoAsync(data);

            return new OkObjectResult(createdTodo);
        }
    }
}
