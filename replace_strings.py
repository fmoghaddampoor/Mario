import os
import re

# Define replacements: (file_path, old_text, new_text) or (file_glob, old_regex, new_text)
replacements = [
    # MarioWindow.cs
    ('src/MarioEngine.Desktop/MarioWindow.cs', 
     '"OpenGL context not available until window is loaded"',
     'Resources.Strings.GL_NotAvailable'),
    ('src/MarioEngine.Desktop/MarioWindow.cs',
     '"Super Mario \\u2014 v"',
     'Resources.Strings.Window_Title'),
    ('src/MarioEngine.Desktop/MarioWindow.cs',
     '"Window starting"',
     'Resources.Strings.Window_Starting'),
    
    # MarioWindowInitializer.cs
    ('src/MarioEngine.Desktop/MarioWindowInitializer.cs',
     '"Window opened: {Width}x{Height}, GL {Major}.{Minor}"',
     'Resources.Strings.Window_Opened'),
    ('src/MarioEngine.Desktop/MarioWindowInitializer.cs',
     '"Framebuffer resized: {Width}x{Height}"',
     'Resources.Strings.Framebuffer_Resized'),
    
    # MarioWindowUpdateHandler.cs
    ('src/MarioEngine.Desktop/MarioWindowUpdateHandler.cs',
     '"Splash finished, starting game"',
     'Resources.Strings.Splash_Finished'),
    
    # SplashScreen.cs
    ('src/MarioEngine.Desktop/SplashScreen.cs',
     '"Splash screen created"',
     'Resources.Strings.Splash_Created'),
    ('src/MarioEngine.Desktop/SplashScreen.cs',
     '$"Shader program link failed: {info}"',
     'string.Format(Resources.Strings.Shader_LinkFailed, info)'),
    ('src/MarioEngine.Desktop/SplashScreen.cs',
     '$"Shader compile failed ({type}): {info}"',
     'string.Format(Resources.Strings.Shader_CompileFailed, type, info)'),
    ('src/MarioEngine.Desktop/SplashScreen.cs',
     '$"Splash image not found: {path}"',
     'string.Format(Resources.Strings.Splash_NotFound, path)'),
    
    # Program.cs
    ('src/MarioEngine.Desktop/Program.cs',
     '"Unhandled exception \\u2014 game crashed"',
     'Resources.Strings.Fatal_UnhandledException'),
    ('src/MarioEngine.Desktop/Program.cs',
     '$"Fatal error: {ex.Message}"',
     'string.Format(Resources.Strings.Fatal_ErrorMessage, ex.Message)'),
    
    # Game.Initialize.cs
    ('src/MarioEngine.Core/Game.Initialize.cs',
     '"Game initializing"',
     'Resources.Strings.Game_Initializing'),
    ('src/MarioEngine.Core/Game.Initialize.cs',
     '"Game initialized"',
     'Resources.Strings.Game_Initialized'),
    
    # Game.LoadContent.cs
    ('src/MarioEngine.Core/Game.LoadContent.cs',
     '"Game.LoadContent started"',
     'Resources.Strings.Game_LoadContent_Started'),
    
    # Game.Shutdown.cs
    ('src/MarioEngine.Core/Game.Shutdown.cs',
     '"Game.Shutdown started"',
     'Resources.Strings.Game_Shutdown_Started'),
    ('src/MarioEngine.Core/Game.Shutdown.cs',
     '"Game shutting down"',
     'Resources.Strings.Game_ShuttingDown'),
    
    # LogConfiguration.cs
    ('src/MarioEngine.Core/Logging/LogConfiguration.cs',
     '"Logging initialized. Seq: {Seq}, Loki: {Loki}"',
     'Resources.Strings.Logging_Initialized'),
    
    # GameServiceProvider.cs
    ('src/MarioEngine.Core/DependencyInjection/GameServiceProvider.cs',
     '"DI container initialized"',
     'Resources.Strings.DI_Container_Initialized'),
]

for filepath, old, new in replacements:
    fullpath = os.path.join('C:/Mario', filepath)
    if not os.path.exists(fullpath):
        print(f'MISSING: {fullpath}')
        continue
    with open(fullpath, 'r', encoding='utf-8') as f:
        content = f.read()
    if old in content:
        content = content.replace(old, new)
        with open(fullpath, 'w', encoding='utf-8') as f:
            f.write(content)
        print(f'  {filepath}: replaced')
    else:
        print(f'  {filepath}: NOT FOUND - "{old[:50]}..."')

# Also need to add using directives
# For Desktop files, add 'using MarioEngine.Desktop.Resources;' if not present
# For Core files, add 'using MarioEngine.Core.Resources;' if not present

# I'll handle the using directives manually after this runs
print('\nDone. Add using directives manually for each file.')
