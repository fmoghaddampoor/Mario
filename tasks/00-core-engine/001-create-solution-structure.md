# Task 001 — Create .NET Solution Structure

## Description
Create the .NET solution and project structure for the Mario engine.

## Steps
1. Run dotnet new sln -n MarioEngine
2. Create projects:
   - dotnet new classlib -n MarioEngine.Core
   - dotnet new classlib -n MarioEngine.Game
   - dotnet new console -n MarioEngine.Desktop
3. Add projects to solution
4. Set up project references:
   - Desktop → Core + Game
   - Game → Core
5. Verify dotnet build succeeds

## Files to Create
- MarioEngine.sln
- src/MarioEngine.Core/MarioEngine.Core.csproj
- src/MarioEngine.Game/MarioEngine.Game.csproj
- src/MarioEngine.Desktop/MarioEngine.Desktop.csproj

## Acceptance Criteria
- Solution builds with dotnet build
- Desktop project runs and shows empty window
