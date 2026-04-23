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

    [Given("a populated mortgage file")]
    public void GivenAPopulatedMortgageFile()
    {
        _mortgageFile = ExampleFactory.CreatePopulatedMortgageFile();
    }

    [When("I serialize the mortgage file to MISMO XML")]
    public void WhenISerializeTheMortgageFileToMismoXml()
    {
        _xml = _translator.Serialize(_mortgageFile!);
    }

    [Then("the XML should contain the loan identifier \"(.*)\"")]
    public void ThenTheXmlShouldContainTheLoanIdentifier(string expected)
    {
        Assert.Contains(expected, _xml);
    }

    [Then("the XML should contain the borrower first name \"(.*)\"")]
    public void ThenTheXmlShouldContainTheBorrowerFirstName(string expected)
    {
        Assert.Contains(expected, _xml);
    }

    [Then("the XML should contain the property city \"(.*)\"")]
    public void ThenTheXmlShouldContainThePropertyCity(string expected)
    {
        Assert.Contains(expected, _xml);
    }

    [Given("the sample MISMO XML file")]
    public void GivenTheSampleMismoXmlFile()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "TestData", "sample-loan-application.xml");
        _xml = File.ReadAllText(path);
    }

    [When("I deserialize the MISMO XML into a mortgage file")]
    public void WhenIDeserializeTheMismoXmlIntoAMortgageFile()
    {
        _mortgageFile = _translator.Deserialize(_xml!);
    }

    [Then("the mortgage file loan identifier should be \"(.*)\"")]
    public void ThenTheMortgageFileLoanIdentifierShouldBe(string expected)
    {
        Assert.Equal(expected, _mortgageFile!.LoanApplication.Metadata.LoanIdentifier);
    }

    [Then("the primary borrower first name should be \"(.*)\"")]
    public void ThenThePrimaryBorrowerFirstNameShouldBe(string expected)
    {
        Assert.Equal(expected, _mortgageFile!.LoanApplication.PrimaryBorrower.FirstName);
    }

    [Then("the subject property city should be \"(.*)\"")]
    public void ThenTheSubjectPropertyCityShouldBe(string expected)
    {
        Assert.Equal(expected, _mortgageFile!.LoanApplication.SubjectProperty.Address.City);
    }
}
