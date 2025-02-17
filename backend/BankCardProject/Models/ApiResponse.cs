namespace BankCardProject.Models
{
    public class ApiResponse<T>
    {
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public T Data { get; set; }
        public object Error { get; set; } // Hata detayları için

        public ApiResponse(string message, T data = default, object error = null)
        {
            Message = message;
            Data = data;
            Error = error;
            Timestamp = DateTime.UtcNow;
        }

        public static ApiResponse<T> SuccessResponse(T data)
        {
            return new ApiResponse<T>("İşlem başarılı.", data);
        }

        public static ApiResponse<T> ErrorResponse(string message, string errorType)
        {
            return new ApiResponse<T>(message, default, new { type = errorType });
        }
    }
}

 