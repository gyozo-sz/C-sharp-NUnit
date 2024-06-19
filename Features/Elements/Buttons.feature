@Buttons
Feature: Buttons

Tests for the Buttons page elements

Background: 
	Given I navigated to the Buttons section in the Elements category

@ButtonsClick
Scenario Outline: Clicking buttons on the Buttons Page
	When I <clickType> click the <buttonText> Button
	Then Message with text You have done a click is displayed
Examples: 
	| clickType | buttonText      |
	| left      | Click Me        |
	| right     | Right Click Me  |
	| double    | Double Click Me |

@Browser:Chrome
@Browser:Firefox
@ButtonsClick
Scenario Outline: Clicking multiple buttons on the Buttons Page
	When I left click the Click Me Button
		And I right click the Right Click Me Button
	Then Messages with text You have done a click are displayed