Feature: TodoList
	In order to remember things
	As a person who sometimes forget
	I want ot be able to create list of tasks


Scenario: Add two items
	Given I am on TodoMVC site
	When I add "Learn F#" task
	And I add "Learn Akka" task
	Then I should have 2 items on the list


Scenario: Add three items and remove one
	Given I am on TodoMVC site
	And I have already added three tasks
	When I click delete button on task # 2
	Then I should have 2 items on the list
