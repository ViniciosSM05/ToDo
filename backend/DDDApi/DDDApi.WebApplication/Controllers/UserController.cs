using AutoMapper;
using DDDApi.Domain.Core.Interfaces.Application;
using DDDApi.Domain.Core.Interfaces.Notification;
using DDDApi.Domain.Core.DTO.User;
using DDDApi.WebApplication.Controllers.Base;
using DDDApi.WebApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DDDApi.Domain.Core.Interfaces.Auth;

namespace DDDApi.WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IApplicationUser applicationUser;
        public UserController(IApplicationUser applicationUser, INotification notification) : base(notification)
        {
            this.applicationUser = applicationUser;

        }

        [HttpPost("Save")]
        [AllowAnonymous]
        public Task<ActionResult<ResponseViewModel<UserSaveResponseDTO>>> Save([FromBody] UserSaveDTO obj, CancellationToken cancellationToken)
            => ExecuteAsync(() => applicationUser.Save(obj, cancellationToken));

        [HttpPost("Login")]
        [AllowAnonymous]
        public Task<ActionResult<ResponseViewModel<UserLoginResponseDTO>>> Login([FromBody] UserLoginDTO obj, CancellationToken cancellationToken)
            => ExecuteAsync(() => applicationUser.Login(obj, cancellationToken));
    }
}
