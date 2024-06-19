@Windows
Feature: Windows

A short summary of the feature

@NewWindow
@NewTab
Scenario Outline: Handling the case when new window is opened
	Given I navigated to the Browser Windows section in the Windows category
	When I click the <buttonLabel> button
	Then Sample page is opened in new window
Examples:
	| buttonLabel |
	| New Tab     |
	| New Window  |
