# Changelog

All notable changes to this project will be documented in this file. This includes the following:
- [Version Number] - Release Date: Each release has its version number and release date. Versions follow the Semantic Versioning (SemVer) format.
    - Added: New features or enhancements.
    - Changed: Modifications or improvements to existing features.
    - Deprecated: Features that are still available but will be removed in future releases.
    - Removed: Features or components that have been removed.
    - Fixed: Bug fixes or resolutions to issues.
    - Updated: Updated dependency versions

## [1.0.0] - 2024-08-04
### Added
- Added this file `CHANGELOG.md` to track version changes.

### Updated
- Because maintainability is a key factor. I have updated dotnet version from 6.0 to 7.0 to 8.0. These changes was made because LTS ends for 6.0 in November 2024 and 8.0 is the current LTS version, see https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core . As part of this, I followed https://learn.microsoft.com/en-us/dotnet/core/install/upgrade and checked for breaking changes:
    - https://learn.microsoft.com/en-us/dotnet/core/compatibility/7.0
    - https://learn.microsoft.com/en-us/dotnet/core/compatibility/8.0
- Updated the TodoList.Api.csproj dependencies to their latest patch versions to get the latest fixes.
- The package.json dependencies had 28 vulnerabilities (14 moderate, 12 high, 2 critical) so I update them to get the security fixes.
    - Note: The build tool `react-scripts` will still return vulnerabilities but see https://github.com/facebook/create-react-app/issues/11174 this is expected and it is recommended to shift the dependency to a dev-dependency and instead use `npm audit --production` if wanting to ignore it's false positive vulnerabilities.
