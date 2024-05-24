@Navigation
Feature: Navigation

A short summary of the feature

@TextBox
Scenario: I go to the Text Box page in the Elements section
	Given I am on the Text Box Page
	Then I see the Email input field
		And The page body contains title text 'Text Box'
