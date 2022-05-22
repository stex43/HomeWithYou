using System;
using System.Text.Json;
using HomeWithYou.Client.Models.Errors;
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
                Message = $"The resource id={resourceId} of {target} not found."
            };

            return new ContentResult()
            {
                StatusCode = error.StatusCode,
                Content = JsonSerializer.Serialize(error)
            };
        }
    }
}