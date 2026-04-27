using Reqnroll;
using Mortgage.Model;
using Mortgage.Model.Translators.MISMO;
using Mortgage.Model.Translators.MISMO.Mapping;
using Xunit;

namespace Mortgage.Model.Translators.MISMO.Tests.Steps;

[Binding]
/// <summary>
/// Implements BDD steps that verify MISMO serialization and deserialization behavior.
/// </summary>
public sealed class MismoRoundTripSteps
{
    private readonly MortgageFileMismoTranslator _translator = new();
    private MortgageFile? _mortgageFile;
    private string? _xml;

    /// <summary>
    /// Creates a populated mortgage file for serialization scenarios.
    /// </summary>
    [Given("a populated mortgage file")]
    public void GivenAPopulatedMortgageFile()
    {
        _mortgageFile = ExampleFactory.CreatePopulatedMortgageFile();
    }

    /// <summary>
    /// Serializes the populated mortgage file into MISMO XML.
    /// </summary>
    [When("I serialize the mortgage file to MISMO XML")]
    public void WhenISerializeTheMortgageFileToMismoXml()
    {
        _xml = _translator.Serialize(_mortgageFile!);
    }

    /// <summary>
    /// Verifies that the generated XML contains the expected loan identifier.
    /// </summary>
    /// <param name="expected">The expected loan identifier.</param>
    [Then("the XML should contain the loan identifier \"(.*)\"")]
    public void ThenTheXmlShouldContainTheLoanIdentifier(string expected)
    {
        Assert.Contains(expected, _xml);
    }

    /// <summary>
    /// Verifies that the generated XML contains the expected borrower first name.
    /// </summary>
    /// <param name="expected">The expected borrower first name.</param>
    [Then("the XML should contain the borrower first name \"(.*)\"")]
    public void ThenTheXmlShouldContainTheBorrowerFirstName(string expected)
    {
        Assert.Contains(expected, _xml);
    }

    /// <summary>
    /// Verifies that the generated XML contains the expected subject property city.
    /// </summary>
    /// <param name="expected">The expected property city.</param>
    [Then("the XML should contain the property city \"(.*)\"")]
    public void ThenTheXmlShouldContainThePropertyCity(string expected)
    {
        Assert.Contains(expected, _xml);
    }

    /// <summary>
    /// Loads the sample MISMO XML payload from the test output directory.
    /// </summary>
    [Given("the sample MISMO XML file")]
    public void GivenTheSampleMismoXmlFile()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "TestData", "sample-loan-application.xml");
        _xml = File.ReadAllText(path);
    }

    /// <summary>
    /// Deserializes the loaded MISMO XML into a mortgage file.
    /// </summary>
    [When("I deserialize the MISMO XML into a mortgage file")]
    public void WhenIDeserializeTheMismoXmlIntoAMortgageFile()
    {
        _mortgageFile = _translator.Deserialize(_xml!, options: null);
    }

    /// <summary>
    /// Verifies the deserialized mortgage file contains the expected loan identifier.
    /// </summary>
    /// <param name="expected">The expected loan identifier.</param>
    [Then("the mortgage file loan identifier should be \"(.*)\"")]
    public void ThenTheMortgageFileLoanIdentifierShouldBe(string expected)
    {
        Assert.Equal(expected, _mortgageFile!.LoanApplication.Metadata.LoanIdentifier);
    }

    /// <summary>
    /// Verifies the deserialized primary borrower contains the expected first name.
    /// </summary>
    /// <param name="expected">The expected borrower first name.</param>
    [Then("the primary borrower first name should be \"(.*)\"")]
    public void ThenThePrimaryBorrowerFirstNameShouldBe(string expected)
    {
        Assert.Equal(expected, _mortgageFile!.LoanApplication.PrimaryBorrower.FirstName);
    }

    /// <summary>
    /// Verifies the deserialized subject property contains the expected city.
    /// </summary>
    /// <param name="expected">The expected property city.</param>
    [Then("the subject property city should be \"(.*)\"")]
    public void ThenTheSubjectPropertyCityShouldBe(string expected)
    {
        Assert.Equal(expected, _mortgageFile!.LoanApplication.SubjectProperty.Address.City);
    }
}
