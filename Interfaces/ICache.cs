namespace BroadcastPluginSDK.Interfaces;

public interface ICache
{
    public bool Master { get; set; }
    public List<KeyValuePair<string, string>> CacheReader(List<string> keys);
    public void Write(Dictionary<string, string> data);
    public void Clear();
}