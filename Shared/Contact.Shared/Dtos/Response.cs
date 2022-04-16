using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Contact.Shared.Dtos
{
    public class Response<T>
    {
        public T Data { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }

        [JsonIgnore]
        public bool IsSuccessful { get; set; }

        public List<string> Errors { get; set; }

        public static Response<T>  Success(T data,int StatusCode)
        {
            return new Response<T>
            {
                Data = data,
                StatusCode = StatusCode,
                IsSuccessful = true
            };
        }

        public static Response<T> Success(int StatusCode)
        {
            return new Response<T>
            {
                Data = default(T),
                StatusCode = StatusCode,
                IsSuccessful = true
            };
        }
        public static Response<T> Fail(List<string> errors, int StatusCode)
        {
            return new Response<T>
            {
                Errors = errors,
                StatusCode = StatusCode,
                IsSuccessful = false
            };
        }

        public static Response<T> Fail(string error, int StatusCode)
        {
            return new Response<T>
            {
                Errors = new List<string>() { error},
                StatusCode = StatusCode,
                IsSuccessful = false
            };
        }
    }
}
