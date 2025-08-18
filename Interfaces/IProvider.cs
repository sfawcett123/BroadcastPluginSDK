namespace BroadcastPluginSDK.Interfaces;

public interface IProvider
{
    public event EventHandler<Dictionary<string, string>> DataReceived;
}