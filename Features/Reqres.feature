Feature: ReqRes queries

Tests for some REST API calls for the ReqRes service

Background: 
	Given I initialized a REST connection to RestReq

@Get
@User
@UserList
Scenario: Get List of Users
	When I query the list of users
	Then The response has status code 200
		And The response contains the list of users

@Get
@User
Scenario: Get User By Id
	When I query the data of the user with id 2
	Then The response has status code 200
		And The response contains the data of the queried user

@Get
@User
@Delay
Scenario Outline: Get User By Id - delayed
	When I query the data of the user with id 2 with a <delay> seconds delay
	Then The response has status code 200
		And The response contains the data of the queried user
		And The response was delayed by <delay> seconds
Examples:
	| delay |
	| 3     |

@Get
@User
@Negative
Scenario: Get User By Invalid Id
	When I query the data of the user with id 23
	Then The response has status code 404

@Get
@Color
Scenario: Get Color By Id
	When I query the data of the color with id 2
	Then The response has status code 200
		And The response contains the data of the queried color

@Get
@Color
@ColorList
Scenario: Get List of Colors
	When I query the list of colors
	Then The response has status code 200
		And The response contains the list of colors

@Get
@Color
@Negative
Scenario: Get Color By Invalid Id
	When I query the data of the color with id 23
	Then The response has status code 404

@Post
@Create
@User
Scenario: Create new user
	When I create new user with the following data
	| Name     | Job    |
	| Morpheus | leader |
	Then The response has status code 201
		And The response contains the data of the new user

@Put
@Update
@User
Scenario: Update user using PUT
	When I update user 2 with the following data using PUT
	| Name     | Job    |
	| Morpheus | leader |
	Then The response has status code 200
		And The response contains the updated data of the user

@Patch
@Update
@User
Scenario: Update user using PATCH
	When I update user 2 with the following data using PATCH
	| Name     | Job    |
	| Morpheus | leader |
	Then The response has status code 200
		And The response contains the updated data of the user

@Delete
@User
Scenario: Delete user
	When I delete user 24
	Then The response has status code 204

@Register
@User
Scenario: Register user
	When I register new user with the following data
	| Email              | Password |
	| eve.holt@reqres.in | pistol   |
	Then The response has status code 200
		And The response contains the data of the registered user

@Register
@User
@Negative
Scenario: Register user - missing password
	When I register new user with the following data
	| Email              |
	| eve.holt@reqres.in |
	Then The response has status code 400
		And Response contains error message Missing password

@Register
@User
@Negative
Scenario: Register user - missing email
	When I register new user with the following data
	| Password |
	| pistol |
	Then The response has status code 400
		And Response contains error message Missing email or username

@Login
@User
@Negative
Scenario: Login user - missing password
	When I login user with the following data
	| Email              |
	| eve.holt@reqres.in |
	Then The response has status code 400
		And Response contains error message Missing password

@Login
@User
@Negative
Scenario: Login user - missing email
	When I login user with the following data
	| Password |
	| pistol |
	Then The response has status code 400
		And Response contains error message Missing email or username