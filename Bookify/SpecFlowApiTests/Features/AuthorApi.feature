Feature: AuthorApi
Background:
@ApiTests
Scenario: Get all authors
	#Given I create an Api Client
	Given I create Get Author request	
	And I create Get Author request
	When I execute the request
	Then I see that the status code is 'OK' 

	Scenario Outline: Get all authors with test parameters
	Given I create Get Author request	
	And I create Get Author request
	When I execute the request
	Then I see that the status code is 'StatusCode' 
	Example: 
	| StatusCode |
	| OK	     |
	| BadRequest |