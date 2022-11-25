using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Zip.InstallmentsApi.ExceptionHandler
{
    public static class WebApiExceptionHandler<T>
    {
        public static IActionResult TryCatch(Func<T> func, string friendlyMessage = null)
        {
            try
            {
                if (InheritsIActionResult(func.GetMethodInfo().ReturnType))
                {
                    return (IActionResult)func();
                }

                return new OkObjectResult(func());
            }
            catch (Exception ex)
            {
                var message = string.IsNullOrEmpty(friendlyMessage) ? ex.Message : friendlyMessage;
                var friendlyException = new ApplicationException(message, ex);

                return new ObjectResult(friendlyException) { StatusCode = 500 };
            }
        }

        private static bool InheritsIActionResult(Type type)
        {
            return typeof(IActionResult).IsAssignableFrom(type);
        }
    }
}
