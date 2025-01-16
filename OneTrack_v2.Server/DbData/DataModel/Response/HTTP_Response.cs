using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.Response
{
    public class HTTP_Response
    {
        public int StatusCode { get; set; }

        public object? ObjData { get; set; }

        public string? StatusMessage { get; set; }

    }
}
