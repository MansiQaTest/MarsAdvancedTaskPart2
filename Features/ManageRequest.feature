Feature: Feature6_ManageRequest

A short summary of the feature involving two users "User1" and "User2" with "Skill Owner" and "Requester"


Scenario: 1 Verify that the Requester can see the Send Request list after sending a request
  Given the Skill Owner logs into Mars and creates a skill and logs out
  And the Requester logs into Mars
  When the Requester sends a request to the Skill Owner
  And the Requester navigates to the Sent Requests page
  Then the listing should be displayed with the new details

Scenario: 2 Verify that the Requester can see the Received Request list
  Given the Skill Owner logs into Mars and creates a skill and logs out
  And the Requester logs into Mars
  When the Requester sends a request to the Skill Owner and logs out
  And the Skill Owner logs in and navigates to the Received Requests page
  Then the listing should be displayed with the new details

Scenario: 3 Verify that the Requester can withdraw a request
  Given the Skill Owner logs into Mars and creates a skill and logs out
  And the Requester logs into Mars
  When the Requester sends a request to the Skill Owner
  And the Requester navigates to the Sent Requests page
  And the Requester withdraws the request
  Then the request should no longer appear in the Requester's active request list

Scenario: 4 Verify that the Requester can mark a request as complete
  Given the Skill Owner logs into Mars and creates a skill and logs out
  And the Requester logs into Mars
  When the Requester sends a request to the Skill Owner and logs out
  And the Skill Owner logs into Mars & accepts the request and logs out
  And the Requester logs into Mars & marks the request as complete
  Then the request status should change to "Review" in the Requester's request list

Scenario: 5 Verify that the Requester can review a completed request
  Given the Skill Owner logs into Mars and creates a skill and logs out
  And the Requester logs into Mars
  When the Requester sends a request to the Skill Owner and logs out
  And the Skill Owner logs into Mars & accepts the request and logs out
  And the Requester logs into Mars & marks the request as complete
  Then the request status should change to "Review" in the Requester's request list
  And Logs out
  And the Skill Owner logs into Mars and complete the status and logs out
  When the Requester provides a review for the completed request
  Then the review should be saved "Rating added, thank you!" message should be displayed

Scenario: 6 Verify that the Skill Owner can accept a request from the Requester
  Given the Skill Owner logs into Mars and creates a skill and logs out
  And the Requester logs into Mars
  When the Requester sends a request to the Skill Owner and logs out
  And the Skill Owner logs into Mars & Skill Owner navigates to the Received Requests page
  And the Skill Owner accepts the request
  Then the request status should change to "Accepted" in the Skill Owner's request list

Scenario: 7 Verify that the Skill Owner can decline a request from the Requester
  Given the Skill Owner logs into Mars and creates a skill and logs out
  And the Requester logs into Mars
  When the Requester sends a request to the Skill Owner and logs out
  And the Skill Owner logs into Mars & Skill Owner navigates to the Received Requests page
  And the Skill Owner declines the request
  Then the request status should change to "Declined" in the Skill Owner request list
