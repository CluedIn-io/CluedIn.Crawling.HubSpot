# Migration Notes

```Shell
# Create a new directory for the crawler
md Crawler.HubSpot
cd Crawler.HubSpot

# Initialize the folder as a Git repository
git init
git flow init

# Run the yeoman generator
#  Answers to generator prompts:
#   Yes to Webhooks
#   No to OAuth
docker run --rm -ti -v ${PWD}:/generated cluedin/generator-crawler-template


# TODO Create Git repository for CluedIn.Crawling.HubSpot
# Add Git remote
git remote add origin https://github.com/CluedIn-io/CluedIn.Crawling.HubSpot.git
git push -u origin master

# TODO fix all the broken bits for dotnet build to run

# .NET Build
dotnet build
dotnet test
dotnet pack

# TODO add coverlet to generate test code coverage statistics from build

```

## Issues Identified

* [ ] `dotnet build` fails after yeoman generator has been run

* [ ] `C:\windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe` fails after yeoman generator has been run

* [ ] update `yeoman-crawler-template` repository to use latest `crawler-template` as a Git sub-module

  * PR #15 failing test. Ref: <https://github.com/CluedIn-io/yeoman-crawler-template/pull/15>
  * Requires `Node.js` version 12
  * Requires `gulp` ... `npm install gulp`
    * [X] `npm` found 10 package vulnerabilities (4 moderate, 6 high). Ref <https://github.com/CluedIn-io/yeoman-crawler-template/issues/16>
