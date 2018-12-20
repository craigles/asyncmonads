using Microsoft.AspNetCore.Mvc;

namespace AsyncMonads.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult InternalServerError(this ControllerBase controller, string description)
        {
            return controller.StatusCode(500, description);
        }
    }
}