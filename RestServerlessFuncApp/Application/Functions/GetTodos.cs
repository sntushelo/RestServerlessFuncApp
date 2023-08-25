using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using RestServerlessFuncApp.Core.Services;

namespace RestServerlessFuncApp.Application.Functions
{
    public class GetTodos
    {
        readonly ITodosService todosServices;
        public GetTodos(ITodosService dataContext)
        {
            todosServices = dataContext;
        }

        [FunctionName("GetTodos")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "todos/")] HttpRequest req)
        {
            var todos = await todosServices.GetTodosAsync();
            return new OkObjectResult(todos);
        }
    }
}
