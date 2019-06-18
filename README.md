# CluedIn.Crawling.HubSpot

CluedIn crawler for HubSpot.

[![Build Status](https://dev.azure.com/CluedIn-io/CluedIn%20Crawlers/_apis/build/status/CluedIn-io.CluedIn.Crawling.HubSpot?branchName=master)](https://dev.azure.com/CluedIn-io/CluedIn%20Crawlers/_build/latest?definitionId=31&branchName=master)

------

## Overview

This repository contains the code and associated tests for the [HubSpot](https://developers.hubspot.com/docs/overview) crawler.

## Working with the Code

Load [Crawling.Hubspot.sln](.\Crawling.Hubspot.sln) in Visual Studio or your preferred development IDE.

### Running Tests

A mocked environment is required to run `integration` and `acceptance` tests. The mocked environment can be built and run using the following [Docker](https://www.docker.com/) command:

```Shell
docker-compose up --build -d
```

To run all `unit` and `integration` tests

```Shell
dotnet test .\Crawling.Hubspot.sln
```

To run only `integration` tests

```Shell
dotnet test .\test\integration\Crawling.Hubspot.Integration.Test\
```

To run [Pester](https://github.com/pester/Pester) `acceptance` tests

```PowerShell
invoke-pester test\acceptance
```

To review the [WireMock](http://wiremock.org/) HTTP proxy logs

```Shell
docker-compose logs wiremock
```

## References

* [HubSpot](https://developers.hubspot.com/docs/overview)

### Tooling

* [Docker](https://www.docker.com/)
* [Pester](https://github.com/pester/Pester)
* [WireMock](http://wiremock.org/)
