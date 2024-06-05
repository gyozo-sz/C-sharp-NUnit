using NUnit_practice.DataTypes;
using RestSharp;

namespace NUnit_practice.Rest
{
    internal class ReqresConnection : RestConnection
    {
        private const string ReqResUrl = "https://reqres.in";
        private const string UsersEndpoint = "api/users";
        private const string ColorsEndpoint = "api/unknown";
        private const string RegisterEndpoint = "api/register";
        private const string LoginEndpoint = "api/login";

        private static string UserByIdEndpoint(int id) => UsersEndpoint + $"/{id}";
        private static string ColorByIdEndpoint(int id) => ColorsEndpoint + $"/{id}";

        public ReqresConnection() : base(ReqResUrl) { }

        public RestResponse? GetUserList()
        {
            CreateRequest(UsersEndpoint, Method.Get);
            return SendRequest();
        }

        private void AddDelayToRequest(int delay)
        {
            AddQueryParameter("delay", delay.ToString());
        }

        public RestResponse? GetUserById(int userId, int? delay = null)
        {
            CreateRequest(UserByIdEndpoint(userId), Method.Get);
            if (delay is not null)
            {
                AddDelayToRequest(delay.Value);
            }
            return SendRequest();
        }

        public RestResponse? GetColorList()
        {
            CreateRequest(ColorsEndpoint, Method.Get);
            return SendRequest();
        }

        public RestResponse? GetColorById(int colorId)
        {
            CreateRequest(ColorByIdEndpoint(colorId), Method.Get);
            return SendRequest();
        }

        public RestResponse? CreateNewUserRequest(CreateUserData createUserData)
        {
            CreateRequest(UsersEndpoint, Method.Post, createUserData);
            return SendRequest();
        }

        public RestResponse? UpdateUser(int userId, CreateUserData updateUserData, Method method = Method.Put)
        {
            CreateRequest(UserByIdEndpoint(userId), method, updateUserData);
            return SendRequest();
        }

        public RestResponse? DeleteUser(int userId)
        {
            CreateRequest(UserByIdEndpoint(userId), Method.Delete);
            return SendRequest();
        }

        public RestResponse? RegisterUser(LoginUserData registerUserData)
        {
            CreateRequest(RegisterEndpoint, Method.Post, registerUserData);
            return SendRequest();
        }

        public RestResponse? LoginUser(LoginUserData loginUserData)
        {
            CreateRequest(LoginEndpoint, Method.Post, loginUserData);
            return SendRequest();
        }
    }
}
