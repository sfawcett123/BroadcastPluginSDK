using System.Diagnostics;
using BroadcastPluginSDK.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BroadcastPluginSDK.abstracts;

public abstract class BroadcastCacheBase : BroadcastPluginBase, ICache
{
    protected BroadcastCacheBase(
        IConfiguration? configuration = null,
        IInfoPage? infoPage = null,
        Image? icon = null,
        string? name = null,
        string? stanza = null,
        string? description = null) : base(configuration, infoPage, icon, name, stanza, description)
    {
        if (configuration != null)
        {
            Debug.WriteLine( "Configuration");
            var config = configuration.GetSection(stanza ?? "Cache").GetChildren();
            var masterSection = config.FirstOrDefault(section => section.Key == "master");
            bool.TryParse(masterSection?.Value, out bool master);
            Master = master;
        }
        else
        {
            Master = false;
        }

    }

    public bool Master { get; set; }
    public abstract List<KeyValuePair<string, string>> CacheReader(List<string> keys);
    public abstract void Write(Dictionary<string, string> data);
    public abstract void Clear();
}