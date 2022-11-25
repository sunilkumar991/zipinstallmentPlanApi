using Microsoft.AspNetCore.Mvc;
using Zip.InstallmentsApi.ExceptionHandler;

namespace Zip.InstallmentsApi.ZipBase
{
    public class BaseClass : ControllerBase
    {
        protected JsonResult Json(object data)
        {
            return new JsonResult(data);
        }
        protected IActionResult TryCatch<T>(Func<T> func)
        {
            return WebApiExceptionHandler<T>.TryCatch(func, null);
        }

        protected IActionResult TryCatch<T>(string friendlyMessage, Func<T> func)
        {
            return WebApiExceptionHandler<T>.TryCatch(func, friendlyMessage);
        }

    }
}
