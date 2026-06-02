namespace MarioEngine.Core.Graphics.Debug;

using System;
using System.IO;
using System.Runtime.InteropServices;

/// <summary>
/// Delegate matching RenderDoc's API capture function signature.
/// </summary>
internal delegate void RenderDocCaptureDelegate();

/// <summary>
/// Integration with RenderDoc GPU debugger.
/// Provides runtime capture trigger and connection detection.
/// RenderDoc must be installed and the application launched through it.
/// </summary>
#pragma warning disable CA1810 // Static constructor required for native library loading
internal static class RenderDocIntegration
{
    /// <summary>RenderDoc module handle, or IntPtr.Zero if not available.</summary>
    private static readonly nint _module;

    /// <summary>Function pointer for RenderDoc capture trigger.</summary>
    private static readonly RenderDocCaptureDelegate? _captureFn;

    /// <summary>Initializes static members of the <see cref="RenderDocIntegration"/> class.</summary>
    static RenderDocIntegration()
    {
        var paths = new[]
        {
            "renderdoc.dll",
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "RenderDoc", "renderdoc.dll"),
            "/usr/lib/librenderdoc.so",
            "/usr/local/lib/librenderdoc.so",
        };

        foreach (var path in paths)
        {
            if (File.Exists(path))
            {
                _module = NativeLibrary.Load(path);
                var ptr = NativeLibrary.GetExport(_module, "RENDERDOC_TriggerCapture");
                _captureFn = Marshal.GetDelegateForFunctionPointer<RenderDocCaptureDelegate>(ptr);
                break;
            }
        }
    }

    /// <summary>Gets a value indicating whether RenderDoc is available and loaded.</summary>
    public static bool IsAvailable => _module != nint.Zero && _captureFn != null;

    /// <summary>
    /// Triggers a RenderDoc capture if RenderDoc is available.
    /// </summary>
    public static void TriggerCapture()
    {
        if (_captureFn != null)
        {
            _captureFn();
        }
    }
}
