using BugLog.Application.Interfaces;
using BugLog.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System;

namespace BugLog.Services.Security
{
    public class SystemUserAccessorService : ISystemUserAccessorService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SystemUserAccessorService(IHttpContextAccessor httpContextAccessor) {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public Guid? GetCurrentySystemuUserId() {
            var userId = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if(!string.IsNullOrEmpty(userId)) {
                return new Guid(userId);
            }
            return null;
        }
    }
}