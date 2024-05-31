using NUnit_practice.DataTypes;
using NUnit_practice.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace NUnit_practice.Steps
{
    [Binding]
    internal class Task1Steps
    {
        private ReqresConnection _connection;
        private ResourceList<User>? _response;

        [Given(@"I initialized a REST connection to (.*)")]
        public void GivenIInitializedARESTConnection(string apiUrl)
        {
            _connection = new ReqresConnection();
        }

        [When(@"I query the list of users")]
        public void WhenIQueryTheListOfUsers()
        {
            _connection.CreateRequest("https://reqres.in/api/users", RestSharp.Method.Get);
            _response = _connection.GetUserList();
        }

        [Then(@"I get a response with the list of users")]
        public void ThenIGetAResponseWithTheListOfUsers()
        {
            Console.WriteLine(_response);
        }

    }
}
