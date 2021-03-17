using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookExchangerApi.Models
{
    public class BaseResponse<T>
    {
        public BaseResponse() { }

        public BaseResponse(T data)
        {
            this.Data = data;
            this.StatusCode = 200;
            this.Message = string.Empty;
        }

        public BaseResponse(int statusCode, string message, T data = default(T))
        {
            this.StatusCode = statusCode;
            this.Message = message;
            this.Data = data;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}