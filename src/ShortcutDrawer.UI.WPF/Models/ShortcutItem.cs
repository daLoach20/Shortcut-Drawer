using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutDrawer.UI.WPF.Models;

[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
public partial class ShortcutItem : ShortcutItemBase
{
    [ObservableProperty]
    [JsonProperty("Path")]
    private string? _Path;
}
