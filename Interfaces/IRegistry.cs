using BroadcastPluginSDK.abstracts;

namespace BroadcastPluginSDK.Interfaces;

public interface IPluginRegistry
{
    void Add(IPlugin plugin);
    IReadOnlyList<IPlugin> GetAll();
    public IEnumerable<BroadcastCacheBase>? Caches();
    public IEnumerable<IProvider>? Providers();
    public ICache? MasterCache();
    public void AttachMasterReader();

    public record PluginInfo(string Name, string Version, string FilePath, string Description, string RepositoryUrl);
}