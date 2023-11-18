using System.IO;

namespace Demo1.Directory.Data;

public class DirectoryItem
{
    public string FullPath { get; set; }

    public string Name => Type == DirectoryItemType.Drive ? FullPath : Path.GetFileName(FullPath);

    public DirectoryItemType Type { get; set; }

    public DirectoryItem(string fullPath, DirectoryItemType type)
    {
        FullPath = fullPath;
        Type = type;
    }
}