@Elements
@TextBox
Feature: TextBox

Tests for the textbox elements

Background: 
	Given I navigated to the Text Box section in the Elements category

Scenario: Submit text box values 
	When I enter the following data into the text fields
	| FullName        | Email               | CurrentAddress  | PermanentAddress          |
	| Sherlock Holmes | sherlock@holmes.com | London, Big Ben | London, 221B Baker Street |
		And Click the Submit button
	Then Output table appears echoing the entered data
	