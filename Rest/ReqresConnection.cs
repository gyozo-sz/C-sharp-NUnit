using NUnit_practice.DataTypes;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit_practice.Rest
{
    internal class ReqresConnection : RestConnection
    {
        private const string ReqResUrl = "https://reqres.in";
        private const string UserListEndpoint = "api/users";

        public ReqresConnection() : base(ReqResUrl) { }

        public ResourceList<User>? GetUserList()
        {
            CreateRequest(UserListEndpoint, RestSharp.Method.Get);
            return SendRequest<ResourceList<User>>();
        }
    }
}
