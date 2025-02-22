﻿namespace DataModel.Response
{
    public class ReturnResult
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public object? ObjData { get; set; }
        public string? ErrMessage { get; set; }
        public List<string>? Errors { get; set; }

        public ReturnResult()
        {
            Success = false;
            ErrMessage = string.Empty;
            Errors = new List<string>();
        }
    }
}
