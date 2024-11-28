Feature: Feature4_ChangePassword  
 
Scenario: 1 Verify that the user should be able to change password with a valid password
Given User Logs into Mars
And User navigates to ChangePassword page
When The user enters the current password, new password, and confirms the new password using data from ChangePasswordwithvaliddata.json and clicks the Save button
Then a success message "Password Changed Successfully" should be displayed


Scenario: 2 Verify that the user is not able to change password with an incorrect current password
Given User Logs into Mars
And User navigates to ChangePassword page
When The user enters the current password, new password, and confirms the new password using data from Changepasswordwithanincorrectcurrentpassword.json and clicks the Save button
Then an error message "Password Verification Failed" should be displayed

Scenario: 3 Verify that the user is not able to change password with new password and confirm password mismatch
Given User Logs into Mars
And User navigates to ChangePassword page
When The user enters the current password, new password, and confirms the new password using data from Changepasswordwithnewpasswordandconfirmpasswordmismatch.json and clicks the Save button
Then an error message "Passwords does not match" should be displayed


Scenario: 4 Verify that the user is not able to change password with empty fields
Given User Logs into Mars
And User navigates to ChangePassword page
When The user leaves all fields empty using data from Changepasswordwithempty.json and clicks the Save button
Then an error message "Please fill all the details before Submit" should be displayed

Scenario: 5 Verify that the user is not able to change password with the new password being the same as the current password
Given User Logs into Mars
And User navigates to ChangePassword page
When The user enters the current password, new password, and confirms the new password using data from Changepasswordwiththenewpasswordbeingthesameasthecurrentpassword.json and clicks the Save button
Then an error message "Current Password and New Password should not be same" should be displayed

Scenario: 6 Verify that the user is not able to change password with an invalid new password format
Given User Logs into Mars
And User navigates to ChangePassword page
When The user enters the current password, new password, and confirms the new password using data from Changepasswordwithinvaliddata.json and clicks the Save button
Then an error message "New password does not meet the required format" should be displayed


