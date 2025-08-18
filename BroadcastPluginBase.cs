using Microsoft.Extensions.Configuration;
using BroadcastPluginSDK.Properties;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace BroadcastPluginSDK
{
    public abstract class BroadcastCacheBase : BroadcastPluginBase , ICache 
    {
        public bool Master { get; set; } = false;
        public abstract List<KeyValuePair<string, string>> CacheReader(List<string> keys);
        public abstract void Write(Dictionary<string, string> data);
        public abstract void Clear();

        protected BroadcastCacheBase(
            IConfiguration? configuration = null,
            IInfoPage? infoPage = null,
            Image? icon = null,
            string? name = null,
            string? stanza = null,
            bool master = false,
            string? description = null) : base(configuration, infoPage, icon, name, stanza, description)
        {
            Master = master;
        }
    }

    public abstract class BroadcastPluginBase : IPlugin
    {
        
        private Image? _icon;
        private IInfoPage? _infoPage;
        private IConfiguration _configuration;
        private string _stanza ;
        private static Image DefaultImage => Resources.red;
        private MainIcon _mainIcon ;

        public MainIcon MainIcon
        {
            get => _mainIcon; 
            set 
            {
                _mainIcon = value;
                _mainIcon.Click += (s, e) => OnClick();
                _mainIcon.MouseHover += (s, e) => OnHover();
            }
        }
        public IInfoPage InfoPage
        {
            get => _infoPage;
            set => _infoPage = value as InfoPage;
        }

        public virtual GetCacheDataDelegate? GetCacheData { set; get; } = null;
        public virtual Image Icon { get => _icon ?? Resources.red; set => _icon = value; }
        public string FilePath { get; set; } = string.Empty;
        public Assembly DerivedAssembly { get => this.GetType().Assembly; }
        private string? GetAssemblyMetadata(string key)
        {
            return DerivedAssembly
                .GetCustomAttributes<AssemblyMetadataAttribute>()
                .FirstOrDefault(attr => attr.Key == key)
                ?.Value;
        }
        public string RepositoryUrl
        {
            get => GetAssemblyMetadata("RepositoryUrl") ?? String.Empty;
        }

        // New protected constructor for DI
        protected BroadcastPluginBase(
                                        IConfiguration? configuration = null,
                                        IInfoPage? infoPage = null,
                                        Image? icon = null,
                                        string? name = null,
                                        string? stanza = null,
                                        string? description = null)
        {
            _icon = icon ?? DefaultImage;
            _stanza = stanza ?? "base";
            _mainIcon = new MainIcon(this, _icon);
            _infoPage = infoPage ?? new InfoPage
            {
                Icon = _icon,
                Name = name ?? "Unknown Plugin",
                Version = GetAssemblyMetadata("Version") ?? "0.0.0",
                Description = description ?? "No description available."
            };

            if (!string.IsNullOrEmpty(stanza) && configuration != null)
            {
                var section = configuration.GetSection(stanza);
                _configuration = section.Exists() ? section : new ConfigurationBuilder().AddInMemoryCollection().Build();
            }
            else
            {
                _configuration = new ConfigurationBuilder().AddInMemoryCollection().Build();
            }

        }

        public static void DumpConfiguration(IConfiguration config, string indent = "")
        {
            foreach (var section in config.GetChildren())
            {
                var value = section.Value;
                var key = section.Path;

                Debug.WriteLine($"{indent}{key} = {value}");

                // Recurse into children
                DumpConfiguration(section, indent + "  ");
            }
        }
        
        public event EventHandler? Click;
        public event EventHandler? MouseHover;

        internal void OnClick()
        {
            Click?.Invoke(this, EventArgs.Empty);
        }

        internal void OnHover()
        {
            MouseHover?.Invoke(this, EventArgs.Empty);
        }
    }

}
