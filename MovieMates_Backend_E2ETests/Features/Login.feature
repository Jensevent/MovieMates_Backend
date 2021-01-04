Feature: Login

Scenario: Validate Login
	Given I have navigated to "http://localhost:3000/"
	Then I Login with Username "jensevent" and Password "Welkom12345"
	Then I'm redirected to "http://localhost:3000/home"
