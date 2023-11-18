using System.Collections.Generic;
using System.Linq;
using Demo1.Directory.Data;

namespace Demo1.Directory;

public class DirectoryStructure
{
    /// <summary>
    /// 获取所有盘
    /// </summary>
    /// <returns></returns>
    public static List<DirectoryItem> GetLogicalDrives()
    {
        return System.IO.Directory.GetLogicalDrives().Select(driveName => new DirectoryItem
            (fullPath: driveName, type: DirectoryItemType.Drive)).ToList();
    }

    /// <summary>
    /// 获取某个路径下的所有文件和文件夹
    /// </summary>
    /// <param name="fullPath"></param>
    /// <returns></returns>
    public static List<DirectoryItem> GetDirectoryContents(string fullPath)
    {
        var items = new List<DirectoryItem>();
        try
        {
            var dirs = System.IO.Directory.GetDirectories(fullPath);
            if (dirs.Length > 0)
                items.AddRange(dirs.Select(dirPath =>
                    new DirectoryItem(fullPath: dirPath, type: DirectoryItemType.Folder)));

            var fs = System.IO.Directory.GetFiles(fullPath);
            if (fs.Length > 0)
                items.AddRange(
                    fs.Select(filePath => new DirectoryItem(fullPath: filePath, type: DirectoryItemType.File)));
        }
        catch
        {
            // ignored
        }

        return items;
    }
}