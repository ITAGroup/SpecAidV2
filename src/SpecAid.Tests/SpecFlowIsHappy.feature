@BasicSteps
@DatabaseSteps
Feature: SpecFlowIsHappy

Scenario: Should Assert True
	Then assert true

Scenario: Should Migrate Database
	Then database is happy
	Then assert true

Scenario: Should Create Clients
	Given there are clients
		| UniqueId | Name |
		| ITA      | ITA  |
	Then assert true