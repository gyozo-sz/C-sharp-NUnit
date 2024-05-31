Feature: Feature1

A short summary of the feature

Background: 
	Given I initialized a REST connection to https://reqres.in/

@tag1
Scenario: Get List of Users
	When I query the list of users
	Then I get a response with the list of users
