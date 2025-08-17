using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BroadcastPluginSDK
{
    public delegate List<KeyValuePair<string, string>> GetCacheDataDelegate(List<string> keys);
    public class ListEventArgs<T> : EventArgs
    {
        public List<T> Items { get; }

        public ListEventArgs(List<T> items)
        {
            Items = items ?? new List<T>();
        }
    }
    public interface IProvider
    {
        public event EventHandler<Dictionary<string, string>> DataReceived;
    }

    public interface ICache
    {
        public bool Master { get; set; } 
        public List<KeyValuePair<string, string>> CacheReader(List<string> keys);
        public void Write(Dictionary<string, string> data) ;
        public void Clear();

        public string Name { get; set; }
    }

    public  interface IPlugin 
    {
        public string Version { get; } 
        public string Name { get; }
        public string Description { get; }
        public string Stanza { get;}
        public UserControl? InfoPage { get; set; }
        public string FilePath { get; set;  }
        public bool AttachConfiguration<T>(T configuration);
        public string Start();
        public string RepositoryUrl { get; }
        public MainIcon MainIcon { get; }
        public GetCacheDataDelegate? GetCacheData { get; set; }

        event EventHandler Click;
        event EventHandler MouseHover;

        
    }
}
