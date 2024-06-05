using RestSharp;
using SpecFlow.Internal.Json;
using System.Data.Common;
using System.Diagnostics;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace NUnit_practice.Rest
{
    public class IncompleteRequestException : Exception
    {
        public IncompleteRequestException()
        {
        }

        public IncompleteRequestException(string message)
            : base(message)
        {
        }

        public IncompleteRequestException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    internal class RestConnection
    {
        private readonly RestClient _client;
        private const int TimeoutSeconds = 1;
        private RestRequest? _request;

        public int LastResponseTime { get; private set; }

        public RestConnection(string url, RestClientOptions? restClientOptions = null) { 
            if (restClientOptions == null)
            {
                restClientOptions = new RestClientOptions(url)
                {
                    ThrowOnAnyError = true,
                    Timeout = TimeSpan.FromSeconds(TimeoutSeconds)
                };
            }
            _client = new RestClient(url);
            _request = null;
        }

        public void CreateRequest(string endpoint, Method method, object? body = null)
        {
            _request = new RestRequest(endpoint, method);
            if (body != null)
            { 
                _request.AddOrUpdateHeader("Content-Type", "application/json");
                _request.AddBody(body);
            }
        }

        public void AddQueryParameter(string name, string value)
        {
            _request!.AddQueryParameter(name, value);
        }

        public void AddHeader(string name, string value)
        {
            if (_request == null)
            {
                throw new IncompleteRequestException("Request was not initialized before sending");
            }
            _request.AddHeader(name, value);
        }

        public RestResponse? SendRequest()
        {
            if (_request == null)
            {
                throw new IncompleteRequestException("Request was not initialized before sending");
            }

            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine("Request contents: {0}", RequestToString());
            RestResponse response = _client.ExecuteAsync(_request).GetAwaiter().GetResult();
            sw.Stop();

            Console.WriteLine("Response contents: {0}", response.Content);
            LastResponseTime = (int)sw.Elapsed.TotalMilliseconds;
            return response;
        }

        public string RequestToString()
        {
            if (_request == null)
            {
                return "";
            }
            var parameters = _request.Parameters.Select(parameter => new
            {
                name = parameter.Name,
                value = parameter.Value,
                type = parameter.Type.ToString()
            });

            string parameterString = "";
            foreach (var parameter in parameters)
            {
                parameterString += parameter.ToString();
            }

            return $"{{Method: {_request.Method}, Resource: {_request.Resource}, Body: {parameterString} }}";
        }

        public void AddStringBody(string jsonBody) {
            if (_request == null)
            {
                throw new IncompleteRequestException("Request was not initialized before sending");
            }
            _request.AddStringBody(jsonBody, ContentType.Json);
        }

        public static int GetResponseStatusCode(RestResponse response)
        {
            HttpStatusCode statusCode = response.StatusCode;
            return (int)statusCode;
        }

        public static Method ParseHttpMethodString(string method)
        {
            return (method) switch
            {
                "GET" => Method.Get,
                "POST" => Method.Post,
                _ => Method.Get
            };
        }
    }
}
