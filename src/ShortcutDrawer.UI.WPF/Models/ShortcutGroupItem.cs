using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace ShortcutDrawer.UI.WPF.Models;

[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
public partial class ShortcutGroupItem : ShortcutItemBase, IDisposable
{
    private CancellationTokenSource? _cancellationTokenSource;
    private CancellationToken _cancellationToken;

    [ObservableProperty]
    [JsonProperty(nameof(ShortcutItems))]
    private ObservableCollection<ShortcutItem> _shortcutItems = [];

    [ObservableProperty]
    private bool _showShortcuts;

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        _cancellationTokenSource?.Dispose();
    }

    [RelayCommand]
    private void OnMouseEnter()
    {
        ShowShortcuts = true;
        if (_cancellationTokenSource != null)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = null;
        }
    }

    [RelayCommand]
    private void OnMouseLeave()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _cancellationToken = _cancellationTokenSource.Token;
        _ = Task.Run(
            async Task? () =>
            {
                await Task.Delay(1000);
                if (_cancellationToken.IsCancellationRequested == false)
                {
                    ShowShortcuts = false;
                }
            },
            _cancellationToken);
    }
}
