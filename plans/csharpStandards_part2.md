## 13. Structs

- Every struct must be in its own separate file
- No structs nested inside class files
- After completing any task, verify all new/modified structs are in their own files

## 14. Shader Source Code

- GLSL shader source code must NOT be embedded as C# string constants in .cs files
- All shaders must be in separate .vert / .frag / .geom files in a Shaders/ folder
- Shaders are loaded at runtime via File.ReadAllText()
- After completing any task, verify all new/modified shader strings are in separate files

## 15. What NOT to Do

- No hardcoded paths (use AssetManager or config)
- No secrets/keys/credentials in code (use env vars)
- No regions
- No `#pragma warning disable` without comment explaining why
- No empty catch blocks
- No public fields (use properties)
- No `DateTime.Now` (use `DateTime.UtcNow` or `Time.GameTime`)
