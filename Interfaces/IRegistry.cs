using BroadcastPluginSDK.abstracts;

namespace BroadcastPluginSDK.Interfaces;

public interface IPluginRegistry
{
    public bool Restart { get; set; }
    public void Add(IPlugin plugin);
    public IPlugin? Get(string shortname);
    public IPlugin? Get(string shortname, string version);
    IReadOnlyList<IPlugin> GetAll();
    public IEnumerable<BroadcastCacheBase>? Caches();
    public IEnumerable<IProvider>? Providers();
    public ICache? MasterCache();
    public void AttachMasterReader();

    public record PluginInfo(string Name, string Version, string FilePath, string Description, string RepositoryUrl);
}