using Broadcast.Classes;

namespace BroadcastPluginSDK.Interfaces;

public interface IPluginUpdater
{
    public ReleaseListItem[] Releases { get;  }
}