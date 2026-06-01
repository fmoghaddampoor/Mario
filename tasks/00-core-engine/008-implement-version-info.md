# Task 008 — Implement Version Info

## Description
Expose application version information at runtime.

## Steps
1. Create VersionInfo static class
2. Read version from assembly metadata
3. Expose: Major, Minor, Patch, Prerelease, BuildMetadata
4. Add ToString() returning full SemVer string
5. Display version in window title and debug overlay
6. Log version at startup

## Files to Create
- src/MarioEngine.Core/VersionInfo.cs

## Acceptance Criteria
- Version displays as "0.1.0-alpha" in title bar
- Version is logged at startup
- Build number increments in CI
