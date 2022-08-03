using DDDApi.Domain.Core.Interfaces.Notification;
using DDDApi.WebApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DDDApi.WebApplication.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        private readonly INotification notification;
        public BaseController(INotification notification)
        {
            this.notification = notification;
        }

        protected async Task<ActionResult<ResponseViewModel<T>>> ExecuteAsync<T>(Func<Task<T>> action)
        {
            try
            {
                var responseData = await action();
                var response = new ResponseViewModel<T>(notification, responseData);
                var code = response.Success ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest;
                return StatusCode(code, response);
            }
            catch (Exception ex)
            {
                var response = new ResponseViewModel<T>(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

    }
}
