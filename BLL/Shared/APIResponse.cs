
namespace BLL.Shared;

public class APIResponse
{
    public bool IsError { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }
    public int Code { get; set; }
}
