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
    }

    public abstract class BroadcastPluginBase : IPlugin 
    {
        public virtual string Version { get => ((InfoPage)_infoPage).Version; set => ((InfoPage)_infoPage).Version = value; }
        public virtual string Name {  get => ((InfoPage)_infoPage).Name ; set => ((InfoPage)_infoPage).Name = value; }
        public virtual string Description { get => ((InfoPage )_infoPage).Description; set => ((InfoPage)_infoPage).Description = value; }
        public virtual string Stanza => "base";
        public virtual UserControl? InfoPage { get => _infoPage; set => _infoPage = value ?? throw new NullReferenceException(); } 
        public virtual MainIcon MainIcon { get; }
        public  virtual GetCacheDataDelegate? GetCacheData { set; get; } = null;

        private Image? _icon = Resources.red;
        private IConfiguration _configuration;
        private UserControl _infoPage = new InfoPage();

        protected IConfiguration? Configuration
        {
            get => _configuration;
            set => _configuration = value  ?? throw new ArgumentNullException( "Mandatory Parameter",  nameof(IConfiguration));
        }

        public virtual Image Icon
        {
            get => _icon ?? Resources.red;
            set
            {
                _icon = value;
                MainIcon.Icon = value;
                if ( _infoPage is InfoPage x) 
                {
                    x.Icon = value; // Update InfoPage _icon if it exists
                }
            }
        }

        public string FilePath { get; set; } = string.Empty;
        public Assembly DerivedAssembly {  get => this.GetType().Assembly; }
        private string? GetAssemblyMetadata(string key)
        { 
            return DerivedAssembly
                .GetCustomAttributes<AssemblyMetadataAttribute>()
                .FirstOrDefault(attr => attr.Key == key)
                ?.Value;
        }

        public string RepositoryUrl { 
            get => GetAssemblyMetadata("RepositoryUrl") ?? String.Empty ;
        
        }
        protected BroadcastPluginBase()
        {
          MainIcon = new MainIcon( this , _icon);
          if (_infoPage is InfoPage x)
          {
              x.Icon = _icon;
              x.Name = "Base Plugin";
              x.Description = "This is a base plugin for the Broadcast system.";
              x.Version = DerivedAssembly.GetName().Version?.ToString() ?? "1.0.0";
          }

          _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection() // Start with an empty configuration
                .Build();
          
        }
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
