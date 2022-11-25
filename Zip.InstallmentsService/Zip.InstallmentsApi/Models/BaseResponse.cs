namespace Zip.InstallmentsApi.Models
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            StateCode = (int)ErrorType.Ok;
            ErroList = new List<string>();

        }
        public string Successmessage {get; set;}
        public int StateCode {get; set;}
        public List<string> ErroList {get; set;}
    }
}
