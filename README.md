# Mortgage Model + MISMO Translators

`mortgage-model` is a .NET 10 reference implementation for representing mortgage loan applications as clean domain objects and exchanging those objects through a dedicated MISMO XML translation layer.

The repository separates business-focused mortgage concepts from standards-specific serialization concerns. The core model remains readable for application code, while MISMO element names, containers, and value mappings stay isolated in translator code.

## Repository contents

- `MortgageModel.slnx` — solution file for the domain model, translator, and tests.
- `src/Mortgage.Model` — core mortgage domain model for borrowers, loan terms, property collateral, assets, liabilities, income, compliance, underwriting, and extensibility metadata.
- `src/Mortgage.Model.Translators.MISMO` — MISMO XML serializer/deserializer, translation options, XML constants, and sample object factory.
- `tests/Mortgage.Model.Translators.MISMO.Tests` — Reqnroll and xUnit v3 behavioral tests for MISMO translation scenarios.
- `MISMO XML` — sample MISMO XML payloads used for local exploration and test data.
- `schemas` — placeholder location for licensed MISMO schema artifacts.

## Design principles

- Keep the domain model business-oriented and independent from XML transport details.
- Isolate MISMO-specific structure, naming, and value conversion in translator classes.
- Use nullable properties for fields that may be absent in partial applications or inbound XML.
- Preserve extensibility through `DomainEntity.Extensions` for partner-specific or unmapped values.
- Keep translation behavior incremental so additional MISMO containers can be mapped over time.

## Target framework and tooling

- Target framework: `net10.0`
- C# language version: preview
- Nullable reference types: enabled
- Solution format: `.slnx`
- Recommended IDE: Visual Studio 2026 or newer with the .NET 10 SDK installed

## Domain model overview

The `Mortgage.Model` project centers on `MortgageFile`, which contains a `LoanApplication` aggregate.

Important model areas include:

- `ApplicationMetadata` and `IntegrationMetadata` for identifiers, source-system context, and exchange metadata.
- `LoanTerms`, `TransactionDetail`, and `HousingExpense` for requested loan terms and transaction amounts.
- `Borrower`, `Demographics`, `DeclarationSet`, `Employment`, and `IncomeItem` for borrower profile data.
- `PropertySubject` and `Address` for collateral and address information.
- `Asset` and `Liability` for qualification and underwriting inputs.
- `ComplianceDetail`, `UnderwritingInfo`, and `GovernmentMonitoringInformation` for compliance and decisioning outputs.
- `ValidationIssue` for data-quality results.

Most domain types inherit from `DomainEntity`, which provides a generated `Id` and an `Extensions` dictionary for unmapped or partner-specific attributes.

## MISMO translation overview

The `MortgageFileMismoTranslator` class maps between domain objects and a MISMO residential XML payload using the MISMO namespace:

`http://www.mismo.org/residential/2009/schemas`

Supported mapping areas currently include:

- Loan identifiers and application dates.
- Loan purpose, amortization type, note amount, requested amount, rate, and term.
- Purchase price, estimated closing costs, and cash from borrower.
- Borrower names, contact information, current address, role, declarations, and HMDA demographic values.
- Assets and liabilities.
- Subject property type, usage, value, unit count, and address.

## Build

```powershell
dotnet build MortgageModel.slnx
```

## Test

```powershell
dotnet test .\tests\Mortgage.Model.Translators.MISMO.Tests\Mortgage.Model.Translators.MISMO.Tests.csproj
```

## C# examples

### Serialize a `MortgageFile` to MISMO XML

```csharp
using Mortgage.Model.Translators.MISMO;
using Mortgage.Model.Translators.MISMO.Mapping;

var translator = new MortgageFileMismoTranslator();
var mortgageFile = ExampleFactory.CreatePopulatedMortgageFile();

string xml = translator.Serialize(
    mortgageFile,
    new MismoTranslationOptions
    {
        MismoVersion = "3.6.2",
        IndentXml = true
    });

File.WriteAllText("loan.mismo.xml", xml);
```

### Deserialize MISMO XML content to a `MortgageFile`

```csharp
using Mortgage.Model.Translators.MISMO;

var translator = new MortgageFileMismoTranslator();
string xml = File.ReadAllText("loan.mismo.xml");

var mortgageFile = translator.Deserialize(xml, options: null);

Console.WriteLine(mortgageFile.LoanApplication.Metadata.LoanIdentifier);
Console.WriteLine(mortgageFile.LoanApplication.PrimaryBorrower.LastName);
```

### Deserialize a MISMO XML file path to a `LoanApplication`

```csharp
using Mortgage.Model.Loans;
using Mortgage.Model.Translators.MISMO;

var translator = new MortgageFileMismoTranslator();

LoanApplication? loanApplication = translator.Deserialize("loan.mismo.xml");

if (loanApplication is not null)
{
    Console.WriteLine(loanApplication.Metadata.LoanIdentifier);
}
```

### Round-trip a MISMO payload

```csharp
using Mortgage.Model.Translators.MISMO;

var translator = new MortgageFileMismoTranslator();
var mortgageFile = translator.Deserialize(File.ReadAllText("loan.mismo.xml"), options: null);

mortgageFile.LoanApplication.Terms.NoteRatePercent = 6.0m;

var updatedXml = translator.Serialize(mortgageFile);
File.WriteAllText("loan.updated.mismo.xml", updatedXml);
```

## Documentation

The source projects generate XML documentation files during build. Public domain and translator classes, methods, and properties include XML comments intended to support IntelliSense, generated API references, and downstream package consumers.

## MISMO schema note

Official MISMO schemas and related artifacts are distributed under MISMO license terms and should be obtained directly from MISMO.

After download, place the schema package under:

`schemas/mismo-3.6.2/`

## Preparing this repository for GitHub

Before first push, review and update:

1. Repository name and description.
2. License file, if required by your organization.
3. CI workflow files, if needed.
4. Branch protection and CODEOWNERS, if needed.

Then initialize and push:

```powershell
git init
git add .
git commit -m "Initial commit"
git branch -M main
git remote add origin <your-github-repo-url>
git push -u origin main
```
