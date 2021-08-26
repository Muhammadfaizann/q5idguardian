using System;
using System.Collections.Generic;

namespace q5id.guardian.Services.Bases
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public List<string> Errors { get; set; }

        public T ResponseObject { get; set; }

        public int ResponseStatusCode { get; set; }
    }

    public class ApiResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public List<string> Errors { get; set; }

        public int ResponseStatusCode { get; set; }
    }
}
