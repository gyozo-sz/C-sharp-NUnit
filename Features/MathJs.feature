Feature: MathJs queries

Tests for some REST API calls for the MathJs service

Background: 
	Given I initialized a REST connection to MathJs

@Post
@Addition
Scenario Outline: Get result of additions
	When I send <expression> for evaluation using POST request
	Then I get result equal to <expectedOutput>
Examples: 
	| expression  | expectedOutput |
	| 3 + 5       | 8              |
	| -3 + 5      | 2              |
	| (-3) + (-3) | -6             |
	| 3 + 0       | 3              |
	| 0 + (-3)    | -3             |
	| 3 + 2 + 3   | 8              |
	| (3 + 2) + 3 | 8              |
	| 3 + (2 + 3) | 8              |

@Post
@Substraction
Scenario Outline: Get result of substractions
	When I send <expression> for evaluation using POST request
	Then I get result equal to <expectedOutput>
Examples: 
	| expression  | expectedOutput   |
	| 3 - 5       | -2               |
	| -3 - 5      | -8               |
	| (-3) - (-3) | 0                |
	| 3 - 0       | 3                |
	| 0 - (-3)    | 3                |
	| 3 - 2 - 3   | -2               |
	| (3 - 2) - 3 | -2               |
	| 3 - (2 - 3) | 4                |

@Post
@Multiplication
Scenario Outline: Get result of multiplications
	When I send <expression> for evaluation using POST request
	Then I get result equal to <expectedOutput>
Examples: 
	| expression  | expectedOutput   |
	| 3 * 5       | 15               |
	| -3 * 5      | -15              |
	| (-3) * (-3) | 9                |
	| 3 * 0       | 0                |
	| 0 * (-3)    | 0                |
	| 3 * 2 * 3   | 18               |
	| (3 * 2) * 3 | 18               |
	| 3 * (2 * 3) | 18               |

@Post
@Division
Scenario Outline: Get result of divisions
	When I send <expression> for evaluation using POST request
	Then I get result equal to <expectedOutput>
Examples: 
	| expression  | expectedOutput | precision |
	| 3 / 5       | 0.6            | 4         |
	| -3 / 5      | -0.6           | 4         |
	| (-3) / (-3) | 1              | 4         |
	| 0 / (-3)    | 0              | 4         |
	| 0 / 3       | 0              | 4         |
	| 3 / 2 / 3   | 0.5            | 4         |
	| (3 / 2) / 3 | 0.5            | 4         |
	| 3 / (2 / 3) | 4.5            | 4         |

@Post
@Division
@ZeroDivision
Scenario Outline: Evaluate division by zero
	When I send <expression> for evaluation using POST request
	Then I get result equal to <expectedOutput>
Examples: 
	| expression | expectedOutput |
	| 2 / 0      | Infinity       |
	| 2 / -0     | -Infinity      |
	| -2 / 0     | -Infinity      |
	| -2 / -0    | Infinity       |
	| 0/0        | NaN            |

@Get
@SquareRoot
Scenario Outline: Get result of square roots
	When I send <expression> for evaluation with precision <precision> using GET request
	Then I get result equal to <expectedOutput>
Examples: 
	| expression | expectedOutput   | precision |
	| sqrt(0)    | 0                | 4         |
	| sqrt(1)    | 1                | 4         |
	| sqrt(4)    | 2                | 4         |
	| sqrt(2)    | 1.414            | 4         |
	| sqrt(-1)   | i                | 4         |
	| sqrt(i)    | 0.7071 + 0.7071i | 4         |
