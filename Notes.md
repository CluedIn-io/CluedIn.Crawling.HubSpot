# CluedIn.Crawling.Hubspot

TODO simple description

------

TODO build status badge

## Migration Notes

```Shell
# Create a new directory for the crawler
md Crawler.Hubspot
cd Crawler.Hubspot

# Initialize the folder as a Git repository
git init

# Run the yeoman generator
#  Answers to generator prompts:
#   Yes to Webhooks
#   No to OAuth
docker run --rm -ti -v ${PWD}:/generated cluedin/generator-crawler-template

```

## Issues Identified

* [ ] `dotnet build` fails after yeoman generator has been run

* [ ] `C:\windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe` fails after yeoman generator has been run

* [ ] update `yeoman-crawler-template` repository to use latest `crawler-template` as a Git sub-module

  * PR #15 failing test. Ref: <https://github.com/CluedIn-io/yeoman-crawler-template/pull/15>
  * Requires `Node.js` version 12
  * Requires `gulp` ... `npm install gulp`
    * [ ] `npm` found 10 package vulnerabilities (4 moderate, 6 high). Ref <https://github.com/CluedIn-io/yeoman-crawler-template/issues/16>

* [ ] rename `test\unit-test`folder to `test\unit` in `crawler-template` repository
