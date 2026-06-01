# Task E005 — Create C# Coding Standards Document

## Description
Define mandatory C# coding rules for all code written in this project.

## Why This Was Done Ahead of Task List
Needed early so the AI agent and all developers write consistent code from the start.

## What Was Created
- `plans/csharpStandards.md` with 14 rule sections:
  1. Naming (PascalCase, _camelCase, etc.)
  2. Style (Allman braces, var usage)
  3. File headers
  4. Using directives (inside namespace)
  5. Logging (structured, no Console.WriteLine)
  6. DI (constructor injection, no service locator)
  7. Error handling
  8. Hot path performance rules
  9. XML documentation
  10. Async conventions
  11. Testing conventions
  12. Max class size (300 soft, 500 hard)
  13. File organization
  14. Prohibited patterns
- Referenced in AGENTS.md so agent reads before writing code

## Files
- `plans/csharpStandards.md`

## Acceptance Criteria
- Agent reads the standards before writing code
- All new code follows the conventions
