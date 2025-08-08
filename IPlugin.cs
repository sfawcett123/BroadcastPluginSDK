using Microsoft.Extensions.Configuration;

namespace PluginBase
{
    public class PluginEventArgs
    {
        public Image? Icon = null ;
        public string Name { get; set; } = string.Empty;
    }
    public interface IInfo
    {
        public string Version { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Image? Icon {  set; }
    }

    public  interface IPlugin 
    {
        public string Version { get; } 
        public string Name { get; }
        public string Description { get; }
        public string Stanza { get;}
        public InfoPage? InfoPage { get; set; }
        public bool AttachConfiguration<T>(T configuration);
        public void Start();
        public MainIcon MainIcon { get; }
        
        event EventHandler Click;
        event EventHandler MouseHover;

    }
}
