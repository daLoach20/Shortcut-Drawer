using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShortcutDrawer.UI.WPF.Models;

[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
public partial class ShortcutGroupItem : ShortcutItemBase
{
    private bool _canShowAgain = true;

    [ObservableProperty]
    [JsonProperty("ShortcutItems")]
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
    private void OnMouseLeave()
    {
        _ = Task.Run(async Task? () =>
        {
            await Task.Delay(1000);
            ShowShortcuts = false;
            _canShowAgain = true;
        });
    }
}
