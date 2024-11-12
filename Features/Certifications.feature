Feature: Feature2_Certifications

A short summary of the feature


Scenario: 1 Verify user can add a new "Certifications" to the profile
Given User Logs into Mars
And User navigates to Certifications tab
When User adds a new Certifications record from the JSON file
Then the new record should be successfully created based on JSON data

Scenario: 2 Verify user cannot create a Certifications with an empty name
Given User Logs into Mars
And User navigates to Certifications tab
When User adds a new Certifications record from the JSON file where name is empty
Then Error message "Please enter Certification Name, Certification From and Certification Year" should be displayed

Scenario: 3 Verify user cannot create duplicate entries for "Certifications" data
Given User Logs into Mars
And User navigates to Certifications tab
When User adds a new Certifications record from the JSON file
And User tries to add the same Certifications record again
Then Error message "This information is already exist." should be displayed

Scenario: 4 Verify user can add multiple Certificationss from JSON file
Given User Logs into Mars
And User navigates to Certifications tab
When User adds multiple Certifications records from the JSON file
Then All Certifications records should be successfully created

Scenario: 5 Verify user cannot create multiple Certifications records with invalid input
Given User Logs into Mars
And User navigates to Certifications tab
When User adds multiple Certificationss records from the JSON file with invalid input
Then Error message "Please enter valid data." should be displayed

Scenario: 6 Verify user can delete "Certifications" from the profile
Given User Logs into Mars
And User navigates to Certifications tab
When User adds a new Certifications record from the JSON file
And User deletes the Certifications record from the JSON file
Then the Certifications record should be successfully deleted

Scenario: 7 Verify user cannot delete a "Certifications" that is not in the list
Given User Logs into Mars
And User navigates to Certifications tab
When User adds a new Certifications record from the JSON file
And User deletes a Certifications record that is not in the list from the JSON file
Then the Certifications record should not be deleted
