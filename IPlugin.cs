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

    public interface IInfoPage
    {
        Control GetControl(); // Add this method to return a Control representation of the InfoPage.
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
    }

    public  interface IPlugin 
    {
        public MainIcon MainIcon { get; }
        public IInfoPage InfoPage { get; }
        public string FilePath { get; set;  }
        public string RepositoryUrl { get; }
        public GetCacheDataDelegate? GetCacheData { get; set; }
        
        event EventHandler Click;
        event EventHandler MouseHover;
    }
}
