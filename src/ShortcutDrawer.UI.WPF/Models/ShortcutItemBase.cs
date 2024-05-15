using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutDrawer.UI.WPF.Models;

[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
public partial class ShortcutItemBase : ObservableObject
{
    [ObservableProperty]
    [JsonProperty("Name")]
    private string _name;
}
