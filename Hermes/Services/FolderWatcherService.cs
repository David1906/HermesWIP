using Hermes.Models;
using System.IO;
using System;

namespace Hermes.Services;

public class FolderWatcherService
{
    public string Filter { get; set; } = "*.*";
    public event EventHandler<string>? FileCreated;

    private FileSystemWatcher? _watcher;

    public void Start(string path)
    {
        this._settings = settings; 
    }

    public void Start()
    {
        this.ProcessExistingFiles();
        this._watcher = new FileSystemWatcher(this._settings.InputPath);
        this._watcher.Created += this.OnFileCreated;
        this._watcher.EnableRaisingEvents = true;
    }

    private void ProcessExistingFiles(string path)
    {
        foreach (var file in Directory.EnumerateFiles(path))
        {
            if (this.Filter.Contains(Path.GetExtension(file), StringComparison.InvariantCultureIgnoreCase))
            {
                this.FileCreated?.Invoke(this, file);
            }
        }
    }

    private void OnFileCreated(object sender, FileSystemEventArgs e)
    {
        this.FileCreated?.Invoke(this, e.FullPath);
    }

    public void Stop()
    {
        this._watcher?.Dispose();
    }
}