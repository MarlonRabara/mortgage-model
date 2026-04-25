# Mortgage Model + MISMO Translators

`mortgage-model-mismo` is a .NET 10 sample/reference implementation of a clean mortgage domain model with a dedicated MISMO translation layer.

The repository is structured so business-domain objects stay intuitive while MISMO XML serialization/deserialization concerns remain isolated in translator code.

## Repository contents

- `MortgageModel.slnx`
- `src/Mortgage.Model` — core mortgage domain model (borrowers, loans, property, assets, liabilities, compliance)
- `src/Mortgage.Model.Translators.MISMO` — MISMO XML translator and mapping helpers
- `tests/Mortgage.Model.Translators.MISMO.Tests` — Reqnroll + xUnit v3 behavioral tests for translation scenarios
- `MISMO XML` — sample MISMO XML payloads
- `schemas` — placeholder and notes for local MISMO schema placement after license acceptance

## Design principles

- Keep domain models readable and business-oriented.
- Isolate MISMO-specific structures to the translation layer.
- Preserve extensibility for partner-specific or unmapped attributes.
- Enable incremental expansion for additional MISMO containers and mappings.

## Target framework and tooling

- Target framework: `net10.0`
- Solution format: `.slnx`
- Recommended IDE: Visual Studio 2026 or newer with .NET 10 SDK installed

## Build

```powershell
dotnet build MortgageModel.slnx
```

## Test

```powershell
dotnet test .\tests\Mortgage.Model.Translators.MISMO.Tests\Mortgage.Model.Translators.MISMO.Tests.csproj
```

## C# examples: serialize and deserialize MISMO XML

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

### Deserialize MISMO XML to a `MortgageFile`

```csharp
using Mortgage.Model.Translators.MISMO;

var translator = new MortgageFileMismoTranslator();
string xml = File.ReadAllText("loan.mismo.xml");

var mortgageFile = translator.Deserialize(xml);

Console.WriteLine(mortgageFile.LoanApplication.Metadata.LoanIdentifier);
Console.WriteLine(mortgageFile.LoanApplication.PrimaryBorrower.LastName);
```

### Round-trip (deserialize, update, serialize)

```csharp
using Mortgage.Model.Translators.MISMO;

var translator = new MortgageFileMismoTranslator();
var mortgageFile = translator.Deserialize(File.ReadAllText("loan.mismo.xml"));

mortgageFile.LoanApplication.Terms.NoteRatePercent = 6.0m;

var updatedXml = translator.Serialize(mortgageFile);
File.WriteAllText("loan.updated.mismo.xml", updatedXml);
```

## MISMO schema note

Official MISMO schemas and related artifacts are distributed under MISMO license terms and should be obtained directly from MISMO.

After download, place the schema package under:

`schemas/mismo-3.6.2/`

## Preparing this repository for GitHub

Before first push, review and update:

1. Repository name and description
2. License file (if required by your organization)
3. CI workflow files (optional)
4. Branch protection and CODEOWNERS (optional)

Then initialize and push:

```powershell
git init
git add .
git commit -m "Initial commit"
git branch -M main
git remote add origin <your-github-repo-url>
git push -u origin main
```
