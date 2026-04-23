param(
    [Parameter(Mandatory = $false)]
    [string]$Destination = ".\schemas\mismo-3.6.2"
)

Write-Host "Open the official MISMO 3.6.2 reference model page and accept the license form to download the XML Schema package."
Write-Host "Official page: https://www.mismo.org/standards-resources/mismo-product/mismo-version-3.6.2-reference-model"
Write-Host "Once downloaded, extract the package into: $Destination"
Write-Host "This script intentionally avoids automating MISMO's gated license acceptance flow."
