using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Content_Man.Web.Api
{
    internal static class ResponseFormatter
    {
        public static Response FromException(this IResponseFormatter @this, Exception ex)
        {
            return new Response()
            {
                StatusCode = HttpStatusCode.BadRequest,
                ContentType = "text/plain",
                Contents = s => (new System.IO.StreamWriter(s) { AutoFlush = true }).Write(ex.Message)
            };
        }
    }
}