using System;
using System.Text.Json;
using HomeWithYou.Views.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeWithYou.API.Infrastructure
{
    public static class ControllerExtensions
    {
        public static IActionResult NotFoundResult(this ControllerBase controller, string target, string resourceId)
        {
            var error = new Error
            {
                StatusCode = StatusCodes.Status404NotFound,
                Target = $"{target}:{resourceId}",
                Message = $"The \"{resourceId}\" of {target} is not found."
            };

            return new ContentResult()
            {
                StatusCode = error.StatusCode,
                Content = JsonSerializer.Serialize(error)
            };
        }
    }
}