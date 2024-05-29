@Buttons
Feature: Buttons

Tests for the Buttons page elements

@ButtonsClick
Scenario Outline: Clicking buttons on the Buttons Page
	Given I navigated to the Buttons section in the Elements category
	When I <clickType> click the <buttonText> Button
	Then Message with text You have done a click is displayed
Examples: 
	| clickType | buttonText      |
	| left      | Click Me        |
	| right     | Right Click Me  |
	| double    | Double Click Me |

@ButtonsClick
Scenario Outline: Clicking multiple buttons on the Buttons Page
	Given I navigated to the Buttons section in the Elements category
	When I left click the Click Me Button
		And I right click the Right Click Me Button
	Then Messages with text You have done a click are displayed