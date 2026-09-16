# Migrating to Multi-Version Targeting

This document records the migration of `CluedIn.Crawling.HubSpot` from a single-version build to
the multi-version targeting pattern, done directly at the `100.0.0` version baseline (the
intermediate `1.0.0` baseline used earlier in this org-wide effort was retired before this repo was
migrated).

Structurally this repo follows the standard four-project crawler shape (Core / Crawling /
Infrastructure / Provider), matching `CluedIn.Crawling.MasterDataServices` closely - that repo's own
migration doc was used as the primary reference. This was the most involved of the four repos
migrated together in this batch, due to real RestSharp major-version API differences throughout the
Provider's Mesh/GDPR processing code.

---

## Overview

| CluedIn version | .NET TFM | Package suffix |
|---|---|---|
| 4.7.0 | net6.0 | `.470` |
| 4.8.0 | net6.0 | `.480` |
| 5.0.0-beta.* | net10.0 | `.500` |

Packages published per version: `CluedIn.Crawling.HubSpot.Core.<suffix>`,
`CluedIn.Crawling.HubSpot.<suffix>`, `CluedIn.Crawling.HubSpot.Infrastructure.<suffix>`,
`CluedIn.Provider.HubSpot.<suffix>`.

---

## Step 1 — Pipeline (`azure-pipelines.yml`)

Replaced `crawler.build.yml` with `crawler.build.jobs.yml`, switched pool from `windows-latest` to
`ubuntu-22.04`, and parameterized `pipelineTemplateRef`.

---

## Step 2 — `Directory.Build.props`

Standard block: `TargetFramework` honouring `CluedInMultiVersionTargetFramework`,
`DefineConstants` for `CLUEDIN_V47`/`V48`/`V50`, and `PackageId` derived from
`_CluedInVersionOnly` (not a raw pipeline override) - this repo's own Provider/Crawling ->
Infrastructure -> Core `ProjectReference` chain has exactly the bug the org-wide `PackageId` fix
addresses.

---

## Step 3 — `Packages.props` / cross-repo dependency

- `_CluedIn` guarded so the pipeline value wins.
- Standard xunit v2/v3 + AutoFixture split by `CLUEDIN_V50`.
- **`CluedIn.CrawlerIntegrationTesting` is itself multi-version targeted** (same finding as
  `CluedIn.Crawling.AcceptanceTest`, migrated immediately before this repo) - switched the
  reference to the suffixed package id, in both `Packages.props` and a direct
  `PackageReference` in `src/HubSpot.Provider/Provider.HubSpot.csproj` (the only `src` project
  that referenced it directly, unsuffixed).

---

## Step 4 — Test projects

### 4a — Removed the unconditional xunit v3 block from `test/Directory.Build.props`

The original `test/Directory.Build.props` unconditionally included `xunit.v3`/`AutoFixture.Xunit3`
for every test project. Stripped it down to just `IsTestProject`, and added the conditional xunit
v2/v3 + AutoFixture split directly to each of the 4 test csprojs instead - same
CS0433-type-clash rationale documented in the `CluedIn.Crawling.MasterDataServices` doc.
`Crawling.HubSpot.Test.Common` (a plain helper library referenced by the other test projects, no
actual tests of its own) only needed `Moq` - it doesn't use xunit or AutoFixture at all.

### 4b — `GlobalUsings.cs` + explicit `using AutoFixture.Xunit3;` removal

Seven source files across all three real test projects had a direct `using AutoFixture.Xunit3;`.
Removed those and added a `GlobalUsings.cs` per test project instead. The integration test project
and `Crawling.HubSpot.Unit.Test` also needed `global using Xunit.Abstractions;` (`#if !CLUEDIN_V50`)
for their `ITestOutputHelper` usage, which resolves under `Xunit` directly on xunit v3 but under
`Xunit.Abstractions` on xunit v2.

---

## Step 5 — Real RestSharp major-version breakage found and fixed

RestSharp is a different major version per CluedIn generation (106.x on 4.7/4.8's net6.0 vs 114.x on
5.0's net10.0). This repo's source was written against the newer 114.x API shape, so it failed
outright on the net6.0 legs. All of the following were found via real compile failures, not
assumed:

- **`RestClientOptions`** doesn't exist on 106.x - `RestClient` there takes a plain `Uri`/`string`
  constructor. Gated with `#if CLUEDIN_V50` in `HubSpotClient`'s constructor.
- **`Method` enum casing**: `Method.Get`/`.Post`/`.Put`/`.Delete` (114.x, PascalCase) vs
  `Method.GET`/`.POST`/`.PUT`/`.DELETE` (106.x, uppercase). Used at 10 call sites across
  `HubSpotClient` and 5 Mesh/GDPR processor files - centralized into one
  `HubSpotRestMethod` static class (in `HubSpot.Infrastructure`, `public` so the `HubSpot.Provider`
  assembly can use it too) instead of repeating `#if` at every call site.
- **`RestResponse` vs `IRestResponse`**: the non-generic response type returned by
  `IRestClient.ExecuteAsync(RestRequest)` changed from `IRestResponse` (106.x) to the concrete
  `RestResponse` class (114.x). Gated `HubSpotClient.GetRequestResponse<T>`'s parameter type.
- **`_client.Options.BaseUrl`**: on 106.x, `.Options` isn't a property on `IRestClient` at all -
  it resolves to an unrelated `RestClientExtensions.Options<T>(IRestClient, IRestRequest)` extension
  method, producing a real `CS0119` error. Sidestepped entirely by keeping the original base `Uri`
  in a field (`_baseUri`) instead of trying to read it back off the client.
- **`QueryParameter`** doesn't exist on 106.x - replaced with the 106.x equivalent,
  `new Parameter(name, value, ParameterType.QueryString)`, under `#if`.
- **`ISystemNotifications.Publish<T>(command)` (106.x, sync) has no equivalent on 5.0+** for a type
  that isn't `INotification`: `ProviderMessageCommand` implements `IPushCommand`, not `INotification`,
  and `ISystemNotifications.PublishNotificationAsync<T>` is now constrained to `T : INotification`
  (real `CS0311`). Verified via reflection against the actual `CluedIn.Core` 4.8/5.0 NuGet packages
  (not guessed) that `ISystemServiceBus.PublishAsync<T>(T)` has no such constraint on either CluedIn
  generation - added `ISystemServiceBus` as a new constructor dependency and used it for the 5.0+
  path, keeping the pre-5.0 path unchanged. Updated the one test that constructs `HubSpotProvider`
  directly to pass the new parameter.
- **`IRestClient.DownloadData` mockability**: production code (`HubSpotFileFetcher`) calls
  `_client.DownloadData(request)` unconditionally, and it compiles on both RestSharp generations -
  but for different reasons. On 106.x it's a genuine `IRestClient` interface member (directly
  mockable). On 114.x it no longer exists on the interface at all; C# silently resolves the call to
  a `RestClientExtensions.DownloadData(IRestClient, RestRequest)` *static extension method* instead
  (confirmed via reflection on the real RestSharp 114.0.0 package) - Moq cannot intercept extension
  method calls, so a test mocking `DownloadData` directly compiles but fails at runtime on the 5.0
  leg only. The real 114.x interface member the extension method delegates to is
  `DownloadStreamAsync(RestRequest, CancellationToken)` (async, returns `Stream`) - the two affected
  tests in `HubSpotImageFetcherTests.cs` now mock that directly under `#if CLUEDIN_V50`, and the
  114.x-only `DownloadData` unconditionally under `#else`. (The tests were also found to have been
  mocking a stale, never-actually-called `DownloadStreamAsync` signature even before this migration
  - real production code has always called `DownloadData` - so this incidentally fixed a
  pre-existing, silently-dead test double as well.)

---

## Step 6 — `NuGet.Config`

Renamed from `Nuget.config` (two-step `git mv`).

---

## Step 7 — Version baseline (`GitVersion.yml`)

Set `next-version: 100.0` directly - no intermediate `1.0.0` step. No `ignore.commits-before` trick
needed: highest pre-existing tag is `v4.0.0`, well below `100.0`. Verified with a real local
`dotnet-gitversion` run: `MajorMinorPatch` resolves to `100.0.0`.

---

## Verification

- `dotnet build` clean (0 errors) across all 3 test projects x 3 legs (9 combinations).
- `dotnet test` actually runs and passes on all three legs (23/26 passing per leg, 3 pre-existing
  skips unrelated to this migration).
- `dotnet pack` confirms correctly-suffixed `PackageId` and correctly-suffixed `ProjectReference`/
  cross-repo dependencies in the produced nuspec (`CluedIn.Crawling.HubSpot.Infrastructure.470`,
  `CluedIn.CrawlerIntegrationTesting.470`).
- `dotnet-gitversion` resolves `MajorMinorPatch: 100.0.0`.

---

## Checklist

- [x] `azure-pipelines.yml` - switched to `crawler.build.jobs.yml`; pool `ubuntu-22.04`; `pipelineTemplateRef` parameterized
- [x] `Directory.Build.props` - `TargetFramework`/`DefineConstants`/`PackageId` (derived, not overridden)
- [x] `Packages.props` - `_CluedIn` guarded; xunit v2/v3 split; `CluedIn.CrawlerIntegrationTesting` switched to the suffixed package id (in both `Packages.props` and one direct `src` `PackageReference`)
- [x] Test projects - unconditional xunit v3 removed from `test/Directory.Build.props`; per-project conditional split added; `GlobalUsings.cs` added to 3 projects; `Xunit.Abstractions` added where needed
- [x] `HubSpotClient.cs` + 5 Mesh/GDPR processor files - real RestSharp 106.x/114.x API differences found and fixed (`RestClientOptions`, `Method` casing via a new `HubSpotRestMethod` helper, `RestResponse`/`IRestResponse`, base URL access, `QueryParameter`)
- [x] `HubSpotProvider.cs` - `ISystemNotifications.Publish` incompatibility with `ProviderMessageCommand` on 5.0+ fixed via `ISystemServiceBus.PublishAsync` (verified against real CluedIn.Core assemblies via reflection); test constructor call updated
- [x] `HubSpotImageFetcherTests.cs` - Moq-vs-extension-method incompatibility for `DownloadData` on RestSharp 114.x fixed by mocking the real underlying interface member (`DownloadStreamAsync`) on that leg instead
- [x] `NuGet.Config` - renamed from `Nuget.config`
- [x] `GitVersion.yml` - `next-version: 100.0` set directly; no `ignore.commits-before` trick needed
- [x] `docs/100.0.0-release-notes.md` added
- [x] Verified: build+test clean on all three legs; pack produces correctly-suffixed packages and dependencies; `dotnet-gitversion` resolves `100.0.0`
