using BroadcastPluginSDK.abstracts;
using BroadcastPluginSDK.Interfaces;
using Microsoft.Win32;
using System.Diagnostics;

namespace BroadcastPluginSDK.Classes;

public class PluginRegistry : IPluginRegistry
{
    //   public IEnumerable<PluginInfo> GetPluginInfo() =>
    //       _plugins.Select(p => new PluginInfo(p.Name, p.Version, p.FilePath, p.Description, p.RepositoryUrl));

    private readonly List<IPlugin> _plugins = new();

    public void Add(IPlugin plugin)
    {
        _plugins.Add(plugin);
    }

    public IReadOnlyList<IPlugin> GetAll()
    {
        return _plugins;
    }

    public ICache? MasterCache()
    {
        var c = Caches()?.FirstOrDefault(c => c.Master);
        if (c != null) return c;

        Debug.WriteLine("No master cache found.");
        return null;
    }

    public IEnumerable<BroadcastCacheBase>? Caches()
    {
        foreach (var plugin in _plugins)
            if (plugin is ICache c)
                yield return (BroadcastCacheBase)plugin;
    }

    public IEnumerable<IProvider>? Providers()
    {
        foreach (var plugin in _plugins)
            if (plugin is IProvider c)
                yield return c;
    }

    public void AttachMasterReader()
    {
        var master = MasterCache();

        if (master is null) return;

        if (master is BroadcastCacheBase c)
            foreach (var plugin in GetAll())
            {
                Debug.WriteLine($"Attaching cache reader to plugin: {plugin.Name}");
                plugin.GetCacheData = c.CacheReader;
            }
    }
    public record PluginInfo(string Name, string Version, string FilePath, string Description, string RepositoryUrl);
}