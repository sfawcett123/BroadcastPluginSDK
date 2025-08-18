using BroadcastPluginSDK.Classes;

namespace BroadcastPluginSDK.Interfaces;

public interface IPlugin
{
    public string Name { get; }
    public MainIcon MainIcon { get; }
    public IInfoPage InfoPage { get; }
    public string FilePath { get; set; }
    public string RepositoryUrl { get; }
    public GetCacheDataDelegate? GetCacheData { get; set; }

    event EventHandler Click;
    event EventHandler MouseHover;
}