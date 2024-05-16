using Newtonsoft.Json;
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
    private bool _IsAboutToHideDrawer = false;

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
    private bool _showDrawer = false;

    [ObservableProperty]
    private int _height = 0;

    [ObservableProperty]
    private int _width = 0;

    [ObservableProperty]
    private ObservableCollection<ShortcutItemBase> _shortcutItems = [];

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
        List<ShortcutItemBase>? itemsToLoad;
        itemsToLoad = LoadShortcutsUsingDeserialization();

        foreach (var item in itemsToLoad!)
        {
            ShortcutItems.Add(item);
        }
    }

    private static List<ShortcutItemBase>? LoadShortcutsUsingDeserialization()
    {
        List<ShortcutItemBase>? itemsToLoad;
        var settings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
        };
        try
        {
            using var sr = new System.IO.StreamReader("shortcuts.json");
            var doc = sr.ReadToEnd();
            if (string.IsNullOrEmpty(doc) == false)
            {
                itemsToLoad = JsonConvert.DeserializeObject<List<ShortcutItemBase>>(doc, settings);
            }
            else
            {
                itemsToLoad = [];
            }
        }
        catch (Exception)
        {
            itemsToLoad = [];
        }

        return itemsToLoad;
    }

    [RelayCommand]
    private void OnTrayClicked()
    {
        _ = System.Windows.MessageBox.Show("Tray Clicked2");
        _ = Task.Run(async Task? () =>
        {
            var output = new List<ShortcutItemBase>();
            output.AddRange(ShortcutItems);

            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
            };
            SerializeToShortcutsFile(output, settings);

            await Task.Delay(50);
            ShowAqua = true;
            await Task.Delay(2000);
            ShowAqua = false;
        });
    }

    private static void SerializeToShortcutsFile(List<ShortcutItemBase> output, JsonSerializerSettings settings)
    {
        var x = JsonConvert.SerializeObject(output, settings);
        using var fw = new System.IO.StreamWriter("shortcuts.json");
        fw.WriteLine(x);
    }

    [RelayCommand]
    private void OnOpenWindow()
    {
        ShowWindow = true;
    }

    [RelayCommand]
    private static void OnClose()
    {
        Application.Current.Shutdown();
    }

    [RelayCommand]
    private void OnShowDrawer()
    {
        _ = Task.Run(async Task? () =>
        {
            await Task.Delay(550);
            ShowDrawer = true;
            _IsAboutToHideDrawer = false;
        });
    }

    [RelayCommand]
    private void OnHideDrawer()
    {
        _IsAboutToHideDrawer = true;
        _ = Task.Run(async Task? () =>
        {
            await Task.Delay(2000);
            if (_IsAboutToHideDrawer)
            {
                ShowDrawer = false;
            }
        });
    }
}
