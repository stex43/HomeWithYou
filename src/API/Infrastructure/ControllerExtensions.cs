using Microsoft.AspNetCore.Mvc;

namespace HomeWithYou.API.Infrastructure
{
    public static class ControllerExtensions
    {
        public static IActionResult Created(this ControllerBase controller, object result)
        {
            return controller.Created(string.Empty, result);
        }
    }
}