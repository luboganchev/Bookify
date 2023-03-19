Feature: AuthorApi

@ApiTests
Scenario: Get all authors
	#Given I create an Api Client
	Given I create Get Author request
	When I execute the request
	Then I see that the status code is 'OK' 