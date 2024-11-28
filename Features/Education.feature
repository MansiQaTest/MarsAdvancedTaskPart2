Feature: Feature1_Education

A short summary of the feature

Scenario: 1 Verify user can add a new "Education" to the profile
Given User Logs into Mars
And User navigates to Education tab
When User adds a new Education record from the AddEducation.json
Then the new record should be successfully created based on JSON data

Scenario: 2 Verify user cannot create a Education with an empty name
Given User Logs into Mars
And User navigates to Education tab
When User adds a new Education record from the AddEducationDatawithempty.json where name is empty
Then Error message "Please enter all the fields" should be displayed

Scenario: 3 Verify user cannot create duplicate entries for "Education" data
Given User Logs into Mars
And User navigates to Education tab
When User adds a new Education record from the AddEducation.json
And User tries to add the same Education record again from the AddEducationWithDuplicateEntry.json
Then Error message "This information is already exist." should be displayed

Scenario: 4 Verify user can add multiple Educations from JSON file
Given User Logs into Mars
And User navigates to Education tab
When User adds multiple Education records from the CreateEduMultipleData.json
Then All Education records should be successfully created

Scenario: 5 Verify user cannot create multiple Education records with invalid input
Given User Logs into Mars
And User navigates to Education tab
When User adds Education records from the CreateEducationDatawithinvalidinput.json with invalid input
Then Error message "Please enter valid data." should be displayed

Scenario: 6 Verify user can delete "Education" from the profile
Given User Logs into Mars
And User navigates to Education tab
When User adds a new Education record from the AddEducation.json
And User deletes the Education record from the DeleteEduDataWhichisinthelist.json
Then the Education record should be successfully deleted

Scenario: 7 Verify user cannot delete a "Education" that is not in the list
Given User Logs into Mars
And User navigates to Education tab
When User adds a new Education record from the AddEducation.json
And User deletes a Education record that is not in the list from DeleteEduDataWhichisnotinthelist.json
Then the Education record should not be deleted
