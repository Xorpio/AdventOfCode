Feature: View Home Page
  As a user
  I want to be able to view the home page
  So that I can access the main content and navigation options

  Scenario: User visits the home page
    Given the user is on the website
    When the user navigates to the home page
    Then the home page should be displayed
    And the home page should contain the main content sections
    And the home page should have navigation options
