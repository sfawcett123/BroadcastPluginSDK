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
            InfoPage? infoPage = null,
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
        public virtual string Version { get => ((InfoPage)_infoPage).Version; set => ((InfoPage)_infoPage).Version = value; }
        public virtual string Name { get => ((InfoPage)_infoPage).Name; set => ((InfoPage)_infoPage).Name = value; }
        public virtual string Description { get => ((InfoPage)_infoPage).Description; set => ((InfoPage)_infoPage).Description = value; }
        public virtual string Stanza
        {
            get => _stanza;  set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Stanza cannot be null or empty.", nameof(value));
                }
                _stanza = value;
            }
        }
        public virtual UserControl? InfoPage { get => _infoPage; set => _infoPage = value ?? throw new NullReferenceException(); }
        public virtual MainIcon MainIcon { get => _mainicon; set => _mainicon = value; }
        public virtual GetCacheDataDelegate? GetCacheData { set; get; } = null;

        private MainIcon _mainicon;
        private Image? _icon;
        private IConfiguration _configuration;
        private UserControl _infoPage;
        private string _stanza ;

        protected IConfiguration? Configuration
        {
            get => _configuration;
            set => _configuration = value ?? throw new ArgumentNullException("Mandatory Parameter", nameof(IConfiguration));
        }

        public virtual Image Icon
        {
            get => _icon ?? Resources.red;
            set
            {
                _icon = value;
                MainIcon.Icon = value;
                if (_infoPage is InfoPage x)
                {
                    x.Icon = value; // Update InfoPage _icon if it exists
                }
            }
        }

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
            InfoPage? infoPage = null,
            Image? icon = null,
            string? name = null ,
            string? stanza = null,
            string? description = null )
        {
            _icon = icon ?? Resources.red;
            _infoPage = infoPage ?? new InfoPage();
            _mainicon  = new MainIcon(this, _icon);
            ((InfoPage)_infoPage).Name = name ?? String.Empty;
            _stanza = stanza ?? "base";
            ((InfoPage)_infoPage).Description  = description ?? String.Empty;

            if (_infoPage is InfoPage x)
            {
                x.Icon = _icon;
                x.Version = DerivedAssembly.GetName().Version?.ToString() ?? "1.0.0";
            }

            _configuration = configuration ?? new ConfigurationBuilder()
                .AddInMemoryCollection()
                .Build();
        }

        // Keep parameterless constructor for backward compatibility
        protected BroadcastPluginBase() : this(
            null, // configuration
            null, // infoPage
            null, // icon
            null, // name
            null, // stanza
            null  // description
        )
        { }

        public virtual bool AttachConfiguration<T>(T configuration)
        {
            var configSection = configuration as IConfigurationSection;
            if (configSection == null)
            {
                Debug.WriteLine("Base Configuration section is not of type IConfigurationSection.");
                return false;
            }

            Debug.WriteLine($"Base - {Name} Attaching configuration to Plugin.");
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddConfiguration(configSection);
            Configuration = configurationBuilder.Build();
            return true;
        }
        public virtual string Start()
        {
            // Default implementation does nothing, can be overridden by derived classes
            return string.Empty;
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

    public class MainIcon : PictureBox
    {
        public Image Icon { get => this.BackgroundImage ?? Resources.red; set => this.BackgroundImage = value; }
        private readonly BroadcastPluginBase _parent;
        public MainIcon( BroadcastPluginBase parent , Image icon) : base()
        {
            this._parent = parent;
            this.BackgroundImage = icon ?? Properties.Resources.red;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Size = new Size(100, 100);
            this.Name = "API";
            this.TabIndex = 0;
            this.TabStop = false;

            this.Click += OnClick;
            this.MouseHover += OnMouseHover;

        }
        internal virtual void OnClick(object? sender, EventArgs e)
        {
            _parent.OnClick();
        }
        internal virtual void  OnMouseHover(object? sender, EventArgs e)
        {
            _parent.OnHover();
        }
    }
}
