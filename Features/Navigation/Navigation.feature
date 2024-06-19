@Navigation
Feature: Navigation

Testing the navigation menu and their test methods

@TextBox
Scenario: I navigate to the Text Box section in the Elements Category
	Given I am on the Text Box section
	Then The page body contains title text 'Text Box'

@TextBox 
@CheckBox
Scenario: I navigate from the Text Box section To the Check Box section
	Given I am on the Text Box section
	When I navigate to the Check Box section in the Elements category
	Then The page body contains title text 'Check Box'

@Alerts
Scenario: I navigate from the Text Box section To the Alerts section
	Given I am on the Text Box section
	When I navigate to the Alerts section in the Alerts category
	Then The page body contains title text 'Alerts'