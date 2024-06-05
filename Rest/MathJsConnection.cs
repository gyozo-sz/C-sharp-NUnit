using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit_practice.Rest
{
    internal class MathJsConnection : RestConnection
    {
        private const string MathJsUrl = "https://api.mathjs.org/v4/";
        private const string DefaultEndpoint = "";
        private const string ExpressionParameter = "expr";
        private const string PrecisionParameter = "precision";
        private const string UserAgent = "Learning RestSharp";

        public MathJsConnection() : base(MathJsUrl) { }
        private string GetPrecisionParameterString(int? precision)
        {
            if (precision == null)
            {
                return "";
            }

            return $", \"precision\": {precision.Value}";
        }

        private void CreateEvaluateExpressionGet(string expr, int? precision)
        {
            CreateRequest(DefaultEndpoint, Method.Get);
            AddQueryParameter(ExpressionParameter, expr);
            if (precision.HasValue)
            {
                AddQueryParameter(PrecisionParameter, precision.Value.ToString());
            }
        }

        private void CreateEvaluateExpressionPost(string expr, int? precision)
        {
            CreateRequest(DefaultEndpoint, Method.Post);
            string precisionFieldString = GetPrecisionParameterString(precision);
            AddStringBody($"{{ \"expr\": \"{expr}\" {precisionFieldString} }}");
        }

        public RestResponse? SendEvaluateExpressionRequest(string expr, Method method = Method.Get, int? precision = null)
        {
            switch(method)
            {
                case Method.Get:
                    CreateEvaluateExpressionGet(expr, precision);
                    break;
                case Method.Post:
                    CreateEvaluateExpressionPost(expr, precision);
                    break;
                default: throw new ArgumentException($"Unsupported http method provided: " +
                        $"{Enum.GetName(typeof(Method), method)}");
            }
            AddHeader("User-Agent", UserAgent);
            return SendRequest();
        }
    }
}
