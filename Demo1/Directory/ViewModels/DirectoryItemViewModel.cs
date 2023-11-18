using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Demo1.Directory.Data;
using Demo1.Directory.ViewModels.Base;

namespace Demo1.Directory.ViewModels;

public class DirectoryItemViewModel : BaseViewModel
{
    public string FullPath { get; set; }
    public string Name => Type == DirectoryItemType.Drive ? FullPath : Path.GetFileName(FullPath);
    public DirectoryItemType Type { get; set; }

    public ObservableCollection<DirectoryItemViewModel?> Children { get; set; }

    public bool CanExpand => Type != DirectoryItemType.File;

    public bool IsExpanded
    {
        get => Children.Any(child => child != null);
        set
        {
            if (value)
                Expand();
            else
                ClearChildren();
        }
    }

    public ICommand ExpandCommand { get; set; }

    public DirectoryItemViewModel(string fullPath, DirectoryItemType type)
    {
        FullPath = fullPath;
        Type = type;
        ExpandCommand = new RelayCommand(Expand);

        // ClearChildren();
        Children = new ObservableCollection<DirectoryItemViewModel?>();
        if (CanExpand) Children.Add(null);
    }

    private void Expand()
    {
        if (CanExpand)
        {
            Children = new ObservableCollection<DirectoryItemViewModel?>(DirectoryStructure
                .GetDirectoryContents(FullPath)
                .Select(content => new DirectoryItemViewModel(content.FullPath, content.Type)));
        }
    }

    private void ClearChildren()
    {
        Children = new ObservableCollection<DirectoryItemViewModel?>();

        if (CanExpand)
        {
            Children.Add(null);
        }
    }
}