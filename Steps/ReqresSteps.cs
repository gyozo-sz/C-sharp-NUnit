using NUnit_practice.DataTypes;
using NUnit_practice.Rest;
using RestSharp;
using System.Text.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace NUnit_practice.Steps
{
    [Binding]
    internal class ReqresSteps
    {
        private ReqresConnection _connection;
        private RestResponse? _response;

        [Given(@"I initialized a REST connection to RestReq")]
        public void GivenIInitializedARESTConnectionToRestReq()
        {
            _connection = new ReqresConnection();
        }

        [When(@"I query the list of users")]
        public void WhenIQueryTheListOfUsers()
        {
            _response = _connection.GetUserList();
        }

        [Then(@"The response contains the list of users")]
        public void ThenIGetAResponseWithTheListOfUsers()
        {
            UserListQueryResponse? userListQueryResponse =
                JsonSerializer.Deserialize<UserListQueryResponse>(_response!.Content!);
            Console.WriteLine(userListQueryResponse);
        }

        [When(@"I query the data of the user with id ([0-9]+)")]
        public void WhenIQueryTheDataOfTheUserWithId(int userId)
        {
            _response = _connection.GetUserById(userId);
        }

        [When(@"I query the data of the user with id ([0-9]+) with a ([0-9]+) seconds delay")]
        public void WhenIQueryTheDataOfTheUserWithIdWithASecondsDelay(int userId, int delay)
        {
            _response = _connection.GetUserById(userId, delay);
        }

        [Then(@"The response was delayed by ([0-9]+) seconds")]
        public void ThenTheResponseWasDelayedBySeconds(int expectedDelaySeconds)
        {
            Assert.That(_connection.LastResponseTime, Is.EqualTo(expectedDelaySeconds * 1000).Within(500));
        }

        [Then(@"The response contains the data of the queried user")]
        public void ThenIGetAResponseWithTheDataOfTheQueriedUser()
        {
            UserQueryResponse? userQueryByIdResponse =
                JsonSerializer.Deserialize<UserQueryResponse>(_response!.Content!);
            Console.WriteLine(userQueryByIdResponse);
        }

        [Then(@"The response has status code ([0-9]+)")]
        public void ThenTheResponseHasStatusCode(int statusCode)
        {
            Assert.That(RestConnection.GetResponseStatusCode(_response!), Is.EqualTo(statusCode));
        }

        [When(@"I query the data of the color with id ([0-9]+)")]
        public void WhenIQueryTheDataOfTheColorWithId(int colorId)
        {
            _response = _connection.GetColorById(colorId);
        }

        [Then(@"The response contains the data of the queried color")]
        public void ThenTheResponseContainsTheDataOfTheQueriedColor()
        {
            ColorQueryResponse? responseQueryByIdResponse =
                JsonSerializer.Deserialize<ColorQueryResponse>(_response!.Content!);
            Console.WriteLine(responseQueryByIdResponse);
        }

        [When(@"I query the list of colors")]
        public void WhenIQueryTheListOfColors()
        {
            _response = _connection.GetColorList();
        }

        [Then(@"The response contains the list of colors")]
        public void ThenIGetAResponseWithTheListOfColors()
        {
            ColorListQueryResponse? userQueryByIdResponse =
                JsonSerializer.Deserialize<ColorListQueryResponse>(_response!.Content!);
            Console.WriteLine(userQueryByIdResponse);
        }

        [When(@"I create new user with the following data")]
        public void WhenICreateNewUserWithTheFollowingData(Table newUserDataTable)
        {
            CreateUserData newUserData = newUserDataTable.CreateInstance<CreateUserData>();
            _response = _connection.CreateNewUserRequest(newUserData);
        }

        [Then(@"The response contains the data of the new user")]
        public void ThenTheResponseContainsTheDataOfTheNewUser()
        {
            CreateUserResponse? createUserResponse =
                JsonSerializer.Deserialize<CreateUserResponse>(_response!.Content!);
            Console.WriteLine(createUserResponse);
        }

        [When(@"I update user ([0-9]+) with the following data using PUT")]
        public void WhenIUpdateUserWithTheFollowingDataUsingPut(int userId, Table updateUserDataTable)
        {
            CreateUserData updateUserData = updateUserDataTable.CreateInstance<CreateUserData>();
            _response = _connection.UpdateUser(userId, updateUserData);
        }

        [Then(@"The response contains the updated data of the user")]
        public void ThenTheResponseContainsTheUpdatedDataOfTheUser()
        {
            UpdateUserResponse? updateUserResponse =
                JsonSerializer.Deserialize<UpdateUserResponse>(_response!.Content!);
            Console.WriteLine(updateUserResponse);
        }

        [When(@"I update user ([0-9]+) with the following data using PATCH")]
        public void WhenIUpdateUserWithTheFollowingDataUsingPatch(int userId, Table updateUserDataTable)
        {
            CreateUserData updateUserData = updateUserDataTable.CreateInstance<CreateUserData>();
            _response = _connection.UpdateUser(userId, updateUserData, Method.Patch);
        }

        [When(@"I delete user ([0-9]+)")]
        public void WhenIDeleteUser(int userId)
        {
            _response = _connection.DeleteUser(userId);
        }

        [When(@"I register new user with the following data")]
        public void WhenIRegisterNewUserWithTheFollowingData(Table registerUserTable)
        {
            LoginUserData registerUserData = registerUserTable.CreateInstance<LoginUserData>();
            _response = _connection.RegisterUser(registerUserData);
        }

        [Then(@"The response contains the data of the registered user")]
        public void ThenTheResponseContainsTheDataOfTheRegisteredUser()
        {
            RegisterUserResponse? registerUserResponse =
                JsonSerializer.Deserialize<RegisterUserResponse>(_response!.Content!);
            Console.WriteLine(registerUserResponse);
        }

        [Then(@"Response contains error message (.*)")]
        public void ThenResponseContainsErrorMessageMissingPassword(string expectedErrorMessage)
        {
            ErrorResponse? errorResponse =
                JsonSerializer.Deserialize<ErrorResponse>(_response!.Content!);
            Assert.That(errorResponse!.Error, Is.EqualTo(expectedErrorMessage));
        }

        [When(@"I login user with the following data")]
        public void WhenILoginUserWithTheFollowingData(Table loginUserTable)
        {
            LoginUserData loginUserData = loginUserTable.CreateInstance<LoginUserData>();
            _response = _connection.LoginUser(loginUserData);
        }


    }
}
