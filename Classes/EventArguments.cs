namespace BroadcastPluginSDK.Classes;

public delegate List<KeyValuePair<string, string>> GetCacheDataDelegate(List<string> keys);

public class ListEventArgs<T> : EventArgs
{
    public ListEventArgs(List<T> items)
    {
        Items = items ?? new List<T>();
    }

    public List<T> Items { get; }
}