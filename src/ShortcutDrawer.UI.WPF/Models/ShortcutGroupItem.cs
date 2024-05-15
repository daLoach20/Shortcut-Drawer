using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutDrawer.UI.WPF.Models;

public partial class ShortcutGroupItem : ShortcutItemBase
{
    private bool _canShowAgain = true;
    private bool _childMouseOver = false;

    [ObservableProperty]
    private ObservableCollection<ShortcutItem> _shortcutItems = new ObservableCollection<ShortcutItem>();

    [ObservableProperty]
    private bool _showShortcuts;

    [RelayCommand]
    private void OnMouseEnter()
    {
        if (_canShowAgain)
        {
            ShowShortcuts = !ShowShortcuts;
            _canShowAgain = false;
            _ = Task.Run(async Task? () =>
            {
                await Task.Delay(5000);
                _canShowAgain = true;
            });
        }
    }

    [RelayCommand]
    private async void OnMouseLeave()
    {
        _ = Task.Run(async Task? () =>
        { 
            await Task.Delay(100);
            if (!_childMouseOver)
            {
                ShowShortcuts = false;
                _canShowAgain = true;
            }
        });
    }

    [RelayCommand]
    private void OnChildMouseEnter()
    {
        _childMouseOver = true;
    }

    [RelayCommand]
    private void OnChildMouseLeave()
    {
        _childMouseOver = false;
    }
}
