﻿namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public Result(bool isSuccess, string message) : this(isSuccess)
        {
            Message = message;
        }
        public Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
