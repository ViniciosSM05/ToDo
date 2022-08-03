using DDDApi.Domain.Core.Interfaces.Auth;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DDDApi.Infra.Auth
{
    public class AuthInfo : IAuthInfo
    {
        public AuthInfo(IHttpContextAccessor httpContextAccessor)
        {
            Id = Guid.Parse(httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            Name = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "";
            Email = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ?? "";
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
    }
}
