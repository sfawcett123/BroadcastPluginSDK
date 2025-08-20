using BroadcastPluginSDK.Classes;

namespace BroadcastPluginSDK.Interfaces;

public interface IPlugin
{
    public string Name { get; }
    public string ShortName { get; }
    public string Version { get; }
    public string Description { get; }
    public MainIcon MainIcon { get; }
    public IInfoPage InfoPage { get; }
    public string FilePath { get; set; }
    public string RepositoryUrl { get; }
    public GetCacheDataDelegate? GetCacheData { get; set; }

    event EventHandler Click;
    event EventHandler MouseHover;
}