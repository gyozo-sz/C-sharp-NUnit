using RestSharp;
using SpecFlow.Internal.Json;
using System.Text.Json;

namespace NUnit_practice.Rest
{
    public static class RestRequestExtension
    {
        
    }

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
                _request.AddBody(body);
            }
        }

        public void AddHeader(string name, string value)
        {
            if (_request == null)
            {
                throw new IncompleteRequestException("Request was not initialized before sending");
            }
            _request.AddHeader(name, value);
        }

        public string? SendRequest()
        {
            if (_request == null)
            {
                throw new IncompleteRequestException("Request was not initialized before sending");
            }
            Console.WriteLine("Sent request: {0}", RequestToString());
            RestResponse response = _client.GetAsync(_request).GetAwaiter().GetResult();
            Console.WriteLine("Response contents: {0}", response);
            return response.Content;
        }

        public T? SendRequest<T>()
        {
            if (_request == null)
            {
                throw new IncompleteRequestException("Request was not initialized before sending");
            }
            Console.WriteLine("Sent request: {0}", RequestToString());
            T? response = _client.GetAsync<T>(_request).GetAwaiter().GetResult();
            Console.WriteLine("Response contents: {0}", response);
            return response;
        }

        public string RequestToString()
        {
            if (_request == null)
            {
                return "";
            }
            return $"{{Method: {_request.Method}, Resource: {_request.Resource} }}";
        }

        public void AddStringBody(string jsonBody) {
            if (_request == null)
            {
                throw new IncompleteRequestException("Request was not initialized before sending");
            }
            _request.AddStringBody(jsonBody, ContentType.Json);
        }
    }
}
