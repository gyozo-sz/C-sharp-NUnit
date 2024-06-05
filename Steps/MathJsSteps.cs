using NUnit_practice.DataTypes.MathJs;
using NUnit_practice.Rest;
using RestSharp;
using System.Text.Json;
using TechTalk.SpecFlow;

namespace NUnit_practice.Steps
{
    [Binding]
    internal class MathJsSteps
    {
        private MathJsConnection _connection;
        private RestResponse? _response;

        [Given(@"I initialized a REST connection to MathJs")]
        public void GivenIInitializedARESTConnectionToMathJs()
        {
            _connection = new MathJsConnection();
        }

        [When(@"I send (.*) for evaluation using (GET|POST) request")]
        public void ISendExpressionForEvaluationWithGetRequest(string expr, string method)
        {
            Method requestMethod = RestConnection.ParseHttpMethodString(method);
            _response = _connection.SendEvaluateExpressionRequest(expr, requestMethod);
        }

        [When(@"I send (.*) for evaluation with precision ([0-9]+) using (GET|POST) request")]
        public void ISendExpressionForEvaluationWithGetRequest(string expr, int precision, string method)
        {
            Method requestMethod = RestConnection.ParseHttpMethodString(method);
            _response = _connection.SendEvaluateExpressionRequest(expr, requestMethod, precision);
        }

        [Then(@"I get result equal to (.*)")]
        public void ThenIGetResultEqualTo(string result)
        {
            if (_response!.Request.Method == Method.Get)
            {
                Assert.That(_response.Content, Is.EqualTo(result));
            }else
            {
                EvaluationResponse? evaluationResponse =
                JsonSerializer.Deserialize<EvaluationResponse>(_response!.Content!);
                Assert.That(evaluationResponse!.Result, Is.EqualTo(result));
            }
            
        }

    }
}
