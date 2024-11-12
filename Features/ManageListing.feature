Feature: Feature5_ManageListing

A short summary of the feature

Scenario: 1 Verify that the user should be able to edit manage listing
Given User Logs into Mars
And User navigates to ManageListing page
When the user add new listing data from json file
When the user modifies the listing details with valid data from json file
Then the listing should be updated with the new details

Scenario: 2 Verify that the user should be able to edit listing with invalid data
Given User Logs into Mars
And User navigates to ManageListing page
When the user add new listing data from json file
When the user modifies the listing details with invalid data from json file
Then an error message "Please complete the form correctly." should be displayed

Scenario: 3 Verify that the user should be able to edit listing with empty fields
Given User Logs into Mars
And User navigates to ManageListing page
When the user add new listing data from json file
When the user modifies the listing details with empty data from json file
Then an error message "Please complete the form correctly." should be displayed	

Scenario: 4 Verify that the user can view listing details
Given User Logs into Mars
And User navigates to ManageListing page
When the user add new listing data from json file
And the user views a listing from the listing table
Then the listing details should be displayed correctly

Scenario: 5 Verify that the user can delete a listing Which is in the list
Given User Logs into Mars
And User navigates to ManageListing page
When the user add new listing data from json file
When the user deletes an existing listing from the listing table
Then the listing should be removed from the listing table

Scenario: 6 Verify that the user can not delete a listing Which is not in the list
Given User Logs into Mars
And User navigates to ManageListing page
When the user add new listing data from json file
When the user deletes data which is not in the listing table
Then the listing should not be removed from the listing table

Scenario: 7 Verify that the toggle can be disabled for a listing
Given User Logs into Mars
And User navigates to ManageListing page
When the user add new listing data from json file 
And the user disables the toggle for the listing
Then the toggle should be in the disabled state

Scenario: 8 Verify that the toggle can be enabled for a listing
Given User Logs into Mars
And User navigates to ManageListing page
When the user add new listing data from json file 
And the user disables the toggle for the listing
And the user enables the toggle for the listing
Then the toggle should be in the enabled state

 Scenario: 9 Verify user should be able to send request for skill trade
 Given User logs into Mars and navigates to the Profile tab
 And User creates ShareSkill data and logs out from Mars
 When User logs into Mars with Different user
 And User searches for the skill and navigates to the skill details page
 And User writes a message to the skill owner and sends the request
 Then Confirmation message "Requested" should be displayed
  


