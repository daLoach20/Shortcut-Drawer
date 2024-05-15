using ShortcutDrawer.UI.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Screen = System.Windows.Forms.Screen;

namespace ShortcutDrawer.UI.WPF.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private bool _IsInitialized = false;

    public MainWindowViewModel()
    {
        if (!_IsInitialized)
        {
            InitializeViewModel();
        }
    }

    [ObservableProperty]
    private string _applicationTitle = string.Empty;

    [ObservableProperty]
    private bool _showWindow = true;

    [ObservableProperty]
    private bool _showAqua = false;

    [ObservableProperty]
    private int _height = 0;

    [ObservableProperty]
    private int _width = 0;

    [ObservableProperty]
    internal ObservableCollection<ShortcutItemBase> _shortcutItems = new ObservableCollection<ShortcutItemBase>();

    private void InitializeViewModel()
    {
        var allScreens = Screen.AllScreens;
        var primaryScreen = allScreens.Where(s => s.Primary == true).Single();

        LoadShortcutItems();

        Height = primaryScreen.Bounds.Height;
        Width = primaryScreen.Bounds.Width;

        ApplicationTitle = "Shortcut Drawer";
        _IsInitialized = true;
    }

    private void LoadShortcutItems()
    {
        var itemsToLoad = new List<ShortcutItemBase>();
        ShortcutItems.Add(new ShortcutItem() { Name = "Personal - Shortcut Drawer", Path = @"C:\TFS\Development\s\Shortcut-Drawer\src\Shortcut Drawer.sln" });

        var group1 = new ShortcutGroupItem() { Name = "Primary Workspace" };
        group1.ShortcutItems.Add(new ShortcutItem() { Name = "Dev - CustomerInquiry", Path = @"C:\TFS\1\GuiPrograms\Development\Customer Programs\CustomerInquiry\CustomerInquiry.sln" });
        group1.ShortcutItems.Add(new ShortcutItem() { Name = "Main - CustomerInquiry", Path = @"C:\TFS\1\GuiPrograms\Main\Customer Programs\CustomerInquiry\CustomerInquiry.sln" });

        var group2 = new ShortcutGroupItem() { Name = "Secondary Workspace" };
        group2.ShortcutItems.Add(new ShortcutItem() { Name = "Dev - CustomerInquiry", Path = @"C:\TFS\2\GuiPrograms\Development\Customer Programs\CustomerInquiry\CustomerInquiry.sln" });
        group2.ShortcutItems.Add(new ShortcutItem() { Name = "Main - CustomerInquiry", Path = @"C:\TFS\2\GuiPrograms\Main\Customer Programs\CustomerInquiry\CustomerInquiry.sln" });

        ShortcutItems.Add(group1);
        ShortcutItems.Add(group2);
    }

    [RelayCommand]
    private void OnTrayClicked()
    {
        _ = System.Windows.MessageBox.Show("Tray Clicked2");
        _ = Task.Run(async Task? () =>
        {
            await Task.Delay(50);
            ShowAqua = true;
            await Task.Delay(2000);
            ShowAqua = false;
        });
    }

    [RelayCommand]
    private void OnHyperlink()
    {

    }

    [RelayCommand]
    private void OnOpenWindow()
    {
        ShowWindow = true;
    }

    [RelayCommand]
    private void OnClose()
    {
        Application.Current.Shutdown();
    }

}
