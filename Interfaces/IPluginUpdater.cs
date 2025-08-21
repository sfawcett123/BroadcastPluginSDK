using Broadcast.Classes;

namespace BroadcastPluginSDK.Interfaces;

public interface IPluginUpdater
{
    public ReleaseListItem[] Releases { get;  }
    public List<ReleaseListItem> Latest();
    public List<string> Versions( string ShortName );
}