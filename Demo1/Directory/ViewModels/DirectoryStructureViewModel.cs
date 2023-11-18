using System.Collections.ObjectModel;
using System.Linq;

namespace Demo1.Directory.ViewModels;

public class DirectoryStructureViewModel
{
    public ObservableCollection<DirectoryItemViewModel> Items { get; set; }

    public DirectoryStructureViewModel()
    {
        var children = DirectoryStructure.GetLogicalDrives();

        Items = new ObservableCollection<DirectoryItemViewModel>(children
            .Select(content => new DirectoryItemViewModel(content.FullPath, content.Type)));
    }
}