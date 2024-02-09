# CluedIn.Crawling.HubSpot

CluedIn crawler for HubSpot.

[![Build Status](https://dev.azure.com/CluedIn-io/CluedIn%20Crawlers/_apis/build/status/CluedIn-io.CluedIn.Crawling.HubSpot?branchName=master)](https://dev.azure.com/CluedIn-io/CluedIn%20Crawlers/_build/latest?definitionId=31&branchName=master)

------

## Overview

This repository contains the code and associated tests for the [HubSpot](https://developers.hubspot.com/docs/overview) crawler.

## Usage

### NuGet Packages

To use the `HubSpot` crawler and provider with the `CluedIn` server you will have to add the following NuGet packages to the `Providers.csproj` project file:

```PowerShell
Install-Package CluedIn.Crawling.HubSpot

Install-Package CluedIn.Crawling.HubSpot.Core

Install-Package CluedIn.Crawling.HubSpot.Infrastructure

Install-Package CluedIn.Provider.HubSpot
```

The NuGet packages specified are available on the [internal development feed](https://dev.azure.com/CluedIn-io/CluedIn%20Crawlers/_packaging?_a=feed&feed=develop).

### Debugging

To debug the `HubSpot` Provider/Crawler:

- Clone the [CluedIn.Crawling.HubSpot](https://github.com/CluedIn-io/CluedIn.Crawling.HubSpot) repository
- Open `Crawling.HubSpot.sln` in Visual Studio
- Rebuild All
- Copy DLL and PDB files from `\**\bin\debug\net452` to the servers `ServerComponents` folder
- Run CluedIn backend server using `.\build.ps1 run`
- In Visual Studio with the `HubSpot` crawler solution open, use `Debug -> Attach to Process` on `CluedIn.Server.ConsoleHostv2.exe`
- In the UI, add a new configuration for the `HubSpot` provider and invoke `Re-Crawl`

## Working with the Code

Load [Crawling.HubSpot.sln](.\Crawling.HubSpot.sln) in Visual Studio or your preferred development IDE.

### Running Tests

A mocked environment is required to run `integration` and `acceptance` tests. The mocked environment can be built and run using the following [Docker](https://www.docker.com/) command:

```Shell
docker-compose up --build -d
```

Use the following commands to run all `Unit` and `Integration` tests within the repository:

```Shell
dotnet test .\Crawling.HubSpot.sln --filter Unit
dotnet test .\Crawling.HubSpot.sln --filter Integration
```

To run [Pester](https://github.com/pester/Pester) `acceptance` tests

```PowerShell
invoke-pester
```

To review the [WireMock](http://wiremock.org/) HTTP proxy logs

```Shell
docker-compose logs wiremock
```

## References

- [HubSpot](https://developers.hubspot.com/docs/overview)

### Tooling

- [Docker](https://www.docker.com/)
- [Pester](https://github.com/pester/Pester)
- [WireMock](http://wiremock.org/)

# About CluedIn
CluedIn is the Cloud-native Master Data Management Platform that brings data teams together enabling them to deliver the foundation of high-quality, trusted data that empowers everyone to make a difference. 

We're different because we use enhanced data management techniques like [Graph](https://www.cluedin.com/graph-versus-relational-databases-which-is-best) and [Zero Upfront Modelling](https://www.cluedin.com/upfront-versus-dynamic-data-modelling) to accelerate the time taken to prepare data to deliver insight by as much as 80%. Installed in as little as 20 minutes from the [Azure Marketplace](https://azuremarketplace.microsoft.com/en-gb/marketplace/apps/cluedin.azure_cluedin?tab=Overview), CluedIn is fully integrated with [Microsoft Purview](https://www.cluedin.com/product/microsoft-purview-mdm-integration?hsCtaTracking=461021ab-7a38-41a3-93dd-cfe2325dfd35%7Cb835efc0-e9b7-4385-a1b6-75cb7632527b) and the full [Microsoft Fabric](https://www.cluedin.com/microsoft-fabric) suite, making it the preferred choice for [Azure customers](https://www.cluedin.com/microsoft-intelligent-data-platform). 

To learn more about CluedIn, [contact the team](https://www.cluedin.com/discovery-call) today.

[https://www.cluedin.com](https://www.cluedin.com)
