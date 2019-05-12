using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Server.Data
{
    public class ObjectResult : IHttpActionResult
    {
        public object Data;
        public HttpRequestMessage Request;
        public Type DataType;
        public ObjectResult(object data, Type type, HttpRequestMessage request)
        {
            Data = data;
            DataType = type;
            Request = request;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                Content = new ObjectContent(DataType, Data, new JsonMediaTypeFormatter()),
                RequestMessage = Request
            };
            return Task.FromResult(response);
        }
    }
}