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

## Working with the Code

Load [Crawling.HubSpot.sln](.\Crawling.HubSpot.sln) in Visual Studio or your preferred development IDE.

### Running Tests

A mocked environment is required to run `integration` and `acceptance` tests. The mocked environment can be built and run using the following [Docker](https://www.docker.com/) command:

```Shell
cd docker
docker-compose up --build -d
```

Use the following commands to run all `Unit` and `Integration` tests within the repository:

```Shell
dotnet test --filter Unit
dotnet test --filter Integration
```

To run [Pester](https://github.com/pester/Pester) `acceptance` tests

```PowerShell
invoke-pester
```

To review the [WireMock](http://wiremock.org/) HTTP proxy logs

```Shell
docker-compose logs wiremock
```

### Debugging

To debug the `HubSpot` Provider/Crawler:

- Clone the [CluedIn.Crawling.HubSpot](https://github.com/CluedIn-io/CluedIn.Crawling.HubSpot) repository
- Open `Crawling.HubSpot.sln` in Visual Studio
- Rebuild All
- Copy DLL and PDB files from `\**\bin\debug\net452` to the servers `ServerComponents` folder
- Run CluedIn backend server using `.\build.ps1 run`
- In Visual Studio with the `HubSpot` crawler solution open, use `Debug -> Attach to Process` on `CluedIn.Server.ConsoleHostv2.exe`
- In the UI, add a new configuration for the `HubSpot` provider and invoke `Re-Crawl`

## References

- [HubSpot](https://developers.hubspot.com/docs/overview)

### Tooling

- [Docker](https://www.docker.com/)
- [Pester](https://github.com/pester/Pester)
- [WireMock](http://wiremock.org/)
