namespace CHLabs.Functions.Data
{
    public class Result
    {
        public Result(string message, bool success, dynamic data)
        {
            Message = message;
            Success = success;
            Data = data;
        }

        public string Message { get; private set; }
        public bool Success { get; private set; }
        public dynamic Data { get; private set; }
    }
}