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
    internal ObservableCollection<ShortcutItem> _shortcutItems = new ObservableCollection<ShortcutItem>();

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
        ShortcutItems.Add(new ShortcutItem() { Name = "Personal - Shortcut Drawer", Path = @"C:\TFS\Development\s\Shortcut-Drawer\src\Shortcut Drawer.sln" });
        ShortcutItems.Add(new ShortcutItem() { Name = "1Dev - CustomerInquiry", Path = @"C:\TFS\1\GuiPrograms\Development\Customer Programs\CustomerInquiry\CustomerInquiry.sln" });
        ShortcutItems.Add(new ShortcutItem() { Name = "1Main - CustomerInquiry", Path = @"C:\TFS\1\GuiPrograms\Main\Customer Programs\CustomerInquiry\CustomerInquiry.sln" });
        ShortcutItems.Add(new ShortcutItem() { Name = "2Dev - CustomerInquiry", Path = @"C:\TFS\2\GuiPrograms\Development\Customer Programs\CustomerInquiry\CustomerInquiry.sln" });
        ShortcutItems.Add(new ShortcutItem() { Name = "2Main - CustomerInquiry", Path = @"C:\TFS\2\GuiPrograms\Main\Customer Programs\CustomerInquiry\CustomerInquiry.sln" });
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
