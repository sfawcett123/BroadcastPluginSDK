using Microsoft.Extensions.Configuration;

namespace BroadcastPluginSDK
{

    public class PluginData : Dictionary<string, string> 
    {
        
    }

    public interface IProvider
    {
        public event EventHandler<PluginData> DataReceived;
    }

    public interface ICache
    {
        public IEnumerable<KeyValuePair<string, string>> Read(List<string> values);
        public void Write( PluginData data );
        public void Clear();
    }

    public  interface IPlugin 
    {
        public string Version { get; } 
        public string Name { get; }
        public string Description { get; }
        public string Stanza { get;}
        public UserControl? InfoPage { get; set; }
        public bool AttachConfiguration<T>(T configuration);
        public void Start();
        public MainIcon MainIcon { get; }
        
        event EventHandler Click;
        event EventHandler MouseHover;

    }
}
