using Zip.InstallmentsService;

namespace Zip.InstallmentsApi.Models
{
    public class DefaultResponseModel<TResult>: BaseResponse
    {
        public TResult Data { get; set; }
    }
}
