Feature: MISMO translator round trip
  In order to keep the domain model clean
  As a mortgage platform developer
  I want to serialize and deserialize a populated loan application through the MISMO translator

  Scenario: Serialize a populated domain model into MISMO XML
    Given a populated mortgage file
    When I serialize the mortgage file to MISMO XML
    Then the XML should contain the loan identifier "LN-10001"
    And the XML should contain the borrower first name "Marlon"
    And the XML should contain the property city "Sacramento"

  Scenario: Deserialize a MISMO XML file into the domain model
    Given the sample MISMO XML file
    When I deserialize the MISMO XML into a mortgage file
    Then the mortgage file loan identifier should be "LN-10001"
    And the primary borrower first name should be "Marlon"
    And the subject property city should be "Sacramento"
