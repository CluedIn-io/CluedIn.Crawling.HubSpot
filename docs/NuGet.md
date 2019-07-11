# NuGet

Information on using [NuGet](https://www.nuget.org/) with the code repository.

## Pack Commands

To create NuGet package files for the `crawler` and `provider` use the following commands:

```Shell
# Create Crawler NuGet packages
nuget pack src\HubSpot.Crawling -IncludeReferencedProjects -Build -Symbols -Properties Configuration=Release

# Create Provider NuGet packages
nuget pack src\HubSpot.Provider -IncludeReferencedProjects -Build -Symbols -Properties Configuration=Release
```

The following NuGet packages will be produced:

* CluedIn.Crawling.HubSpot.1.0.0.nupkg
* CluedIn.Crawling.HubSpot.1.0.0.symbols.nupkg
* CluedIn.Provider.HubSpot.1.0.0.nupkg
* CluedIn.Provider.HubSpot.1.0.0.symbols.nupkg

Exclude the `-Symbols` command line switch to prevent generation of [symbol packages](https://docs.microsoft.com/en-us/nuget/create-packages/symbol-packages).

*Note:* Care should be taken with the distribution of symbol packages as they contain the _source code_.
