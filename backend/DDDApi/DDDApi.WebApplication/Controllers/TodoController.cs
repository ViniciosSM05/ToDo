using DDDApi.Domain.Core.DTO.Todo;
using DDDApi.Domain.Core.Interfaces.Application;
using DDDApi.Domain.Core.Interfaces.Notification;
using DDDApi.WebApplication.Controllers.Base;
using DDDApi.WebApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDApi.WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : BaseController
    {
        private readonly IApplicationTodo applicationTodo;
        public TodoController(IApplicationTodo applicationTodo, INotification notification) : base(notification)
        {
            this.applicationTodo = applicationTodo;
        }

        [HttpGet("GetTodosByUser/{userId}")]
        [Authorize]
        public Task<ActionResult<ResponseViewModel<List<TodoGridDTO>>>> GetTodosByUser(Guid userId, CancellationToken cancellationToken)
            => ExecuteAsync(() => applicationTodo.GetTodosByUserId(userId, cancellationToken));

        [HttpPost("Save")]
        [Authorize]
        public Task<ActionResult<ResponseViewModel<TodoSaveDTO>>> Save([FromBody] TodoSaveDTO obj, CancellationToken cancellationToken)
            => ExecuteAsync(() => applicationTodo.Save(obj, cancellationToken));

        [HttpDelete("Remove")]
        [Authorize]
        public Task<ActionResult<ResponseViewModel<int>>> Remove([FromBody] Guid id, CancellationToken cancellationToken)
            => ExecuteAsync(() => applicationTodo.Remove(id, cancellationToken));
    }
}
