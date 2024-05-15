using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutDrawer.UI.WPF.Models;

internal partial class ShortcutItem : ObservableObject
{
    [ObservableProperty]
    private string? _Name;

    [ObservableProperty]
    private string? _Path;
}
