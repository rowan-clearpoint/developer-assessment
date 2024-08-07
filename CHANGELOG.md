# Changelog

All notable changes to this project will be documented in this file. This includes the following:
- [Version Number] - Release Date: Each release has its version number and release date. Versions follow the Semantic Versioning (SemVer) format.
    - Added: New features or enhancements.
    - Changed: Modifications or improvements to existing features.
    - Deprecated: Features that are still available but will be removed in future releases.
    - Removed: Features or components that have been removed.
    - Fixed: Bug fixes or resolutions to issues.
    - Updated: Updated dependency versions

## [2.1.0] - 2024-08-07
### Added
- Add the ability to add (POST) a Todo Item by calling the backend API
- The ability to surface any errors from the backend API in the UI. Note 500 errors are intentionally not surfaced to avoid revealing unintended information.
- Added unit test coverage for the backend.

### Changed
- Rearranged the backend project structure to:
```
TodoList.Api/
  Controllers/          # The entry points for handling HTTP requests.
  Middleware/           # Middleware components which are used to handle cross-cutting concerns such as logging
  Models/               # Data models that represent the application's data structures, including validation rules and other attributes.
  Repositories/         # Repository classes handle data access and retrieval from the database providing an abstraction layer between the application logic and the data source.
  Services/             # Service classes implement the business logic of the application
  Validations/          # Custom validation attributes and classes used to enforce business rules and data integrity on data models
```
## [2.0.1] - 2024-08-04
### Changed
- Rearranged the frontend react project structure to:
```
src/
  assets/               # Static assets like images, fonts, etc.
  components/           # Reusable UI components
    common/             # Common reusable components
    todo/               # Specific components for the todo feature
  models/               # TypeScript interfaces and types
    todo/               # Specific models for the todo feature
  pages/                # Page components
  styles/               # Global and module CSS/SCSS files
  App.tsx               # Main app component
  index.tsx             # Entry point for React
  reportWebVitals.ts
  setupTests.ts         # Setup for tests
```

## [2.0.0] - 2024-08-04
### Changed
- The frontend react app has been migrated from javascript to typescript for improvements such as TypeScript's static type checking to help catch errors at compile time rather than runtime, reducing bugs and improving code reliability.

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