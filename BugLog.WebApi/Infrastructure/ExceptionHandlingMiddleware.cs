using System;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using BugLog.Application.Exceptions;


namespace BugLog.WebApi.Infrastructure
{
    public class ExceptionHandlingMiddleware {
    
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task Invoke(HttpContext context) {
            try {
                await _next(context);
            } catch(Exception ex) {
                await HandleExceptionAsync(context, ex);
            }
        }
        
        private Task HandleExceptionAsync(HttpContext context, Exception ex) {
            
            // default http statuscode.
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            // handle custom exceptions.
            switch(ex) {
                case BadRequestException badRequest: {
                    code = HttpStatusCode.BadRequest;
                    result = badRequest.Message;
                } break;

                case ValidationException validationException: {
                    code = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(validationException.Failures);
                } break;
                
                case AuthException authException: {
                    code = HttpStatusCode.Unauthorized;
                    result = authException.Message;
                } break;
                
                case DeleteFailureException deleteFailure: {
                    code = HttpStatusCode.BadRequest;
                    result = deleteFailure.Message;
                } break;

                case DuplicateUserException duplicateUser: {
                    code = HttpStatusCode.BadRequest;
                    result = duplicateUser.Message;
                } break;

                case EntityNotFoundException entityNotFound: {
                    code = HttpStatusCode.NotFound;
                    result = entityNotFound.Message;
                } break;
            }

            context.Response.ContentType = "applicaiton/json";
            context.Response.StatusCode = (int)code;

            if(result == string.Empty) {
                result = JsonConvert.SerializeObject(new { error = ex.Message });
            }
            return context.Response.WriteAsync(result);
        }
    }

    public static class ExceptionHandlingExtension {
            public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder) {
                return builder.UseMiddleware<ExceptionHandlingMiddleware>();
            }
        }
}