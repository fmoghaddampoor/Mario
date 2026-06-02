namespace MarioEngine.Core.Save;

/// <summary>Manages save file backups with a rolling window of 3.</summary>
internal static class SaveBackup
{
    public static void CreateBackup(string slotFile)
    {
        if (!File.Exists(slotFile)) return;

        var dir = Path.GetDirectoryName(slotFile)!;
        var name = Path.GetFileNameWithoutExtension(slotFile);
        var ext = Path.GetExtension(slotFile);

        for (int i = 2; i >= 0; i--)
        {
            var oldPath = Path.Combine(dir, $"{name}.bak{i}{ext}");
            var newPath = Path.Combine(dir, $"{name}.bak{i + 1}{ext}");
            if (File.Exists(oldPath))
            {
                if (i == 2) File.Delete(oldPath);
                else File.Move(oldPath, newPath, overwrite: true);
            }
        }

        File.Copy(slotFile, Path.Combine(dir, $"{name}.bak0{ext}"), overwrite: true);
    }

    public static bool RestoreBackup(string slotFile)
    {
        var dir = Path.GetDirectoryName(slotFile)!;
        var name = Path.GetFileNameWithoutExtension(slotFile);
        var ext = Path.GetExtension(slotFile);
        var backup = Path.Combine(dir, $"{name}.bak0{ext}");

        if (!File.Exists(backup)) return false;
        File.Copy(backup, slotFile, overwrite: true);
        return true;
    }
}
