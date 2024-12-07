public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; } 
    public string Message { get; set; }
    public string Error { get; set; } = string.Empty; 

    public ApiResponse()
    {
        Success = true;
        Error = string.Empty; 
    }

    public ApiResponse(T data, string message = "")
    {
        Success = true;
        Data = data;
        Message = message;
        Error = string.Empty;
    }


    public ApiResponse(string error)
    {
        Success = false;
        Error = error;
        Message = string.Empty; 
        Data = default; 
    }

    
}
