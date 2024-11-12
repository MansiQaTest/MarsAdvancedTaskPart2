Feature: Feature3_ProfileDescription

A short summary of the feature


Scenario: 1 Verify that the user should be able to add a description with a Valid Description Entry
Given User Logs into Mars & navigates to Profile tab
And Edit Description Box
When the user enters a valid description from the JSON file & clicks the Save button
Then The description should be saved successfully based on JSON data
    
Scenario: 2 Verify that the user is not able to add a description with an Empty Description
Given User Logs into Mars & navigates to Profile tab
And Edit Description Box
When the user enters an empty description from the JSON file & clicks the Save button
Then an error message "First character can only be digit or letters" should be displayed

Scenario: 3 Verify that the user should be able to add a description with the Maximum Character Limit
Given User Logs into Mars & navigates to Profile tab
And Edit Description Box
When the user enters a description with the Maximum Character Limit from the JSON file & clicks the Save button
Then Description should be saved successfully based on JSON data

Scenario: 4 Verify that the user is not able to add a description with Exceeding Maximum Character Limit
Given User Logs into Mars & navigates to Profile tab
And Edit Description Box
When the user enters a description with exceed character limit from the JSON file
Then  Maximum Characters should be add in Description box

Scenario: 5 Verify that the user is not able to add a description with Save Without Changes
Given User Logs into Mars & navigates to Profile tab
And Edit Description Box
And User enters a valid description from the JSON file & clicks the Save button
When The user clicks the Save button again without Changes
Then Last Saved Description should be displayed without changes

Scenario: 6 Verify that the user is not able to add a description with HTML/Script Injection Attempt
Given User Logs into Mars & navigates to Profile tab
And Edit Description Box
When the user enters a description from the JSON file & clicks the Save button
Then an error message "First character can only be digit or letters" should be displayed

Scenario: 7 Verify that the user should validate Character Counter
Given User Logs into Mars & navigates to Profile tab
And Edit Description Box
When the user types characters in the description
Then the character counter should update with the remaining characters
