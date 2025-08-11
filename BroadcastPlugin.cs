using Microsoft.Extensions.Configuration;
using BroadcastPluginSDK.Properties;
using System.Diagnostics;
using System.Reflection;

namespace BroadcastPluginSDK
{
    public abstract class BroadcastPlugin : IPlugin 
    {

        public virtual string Version { get => ((InfoPage)_infoPage).Version; set => ((InfoPage)_infoPage).Version = value; }
        public virtual string Name {  get => ((InfoPage)_infoPage).Name ; set => ((InfoPage)_infoPage).Name = value; }
        public virtual string Description { get => ((InfoPage )_infoPage).Description; set => ((InfoPage)_infoPage).Description = value; }
        public virtual string Stanza => "base";
        public virtual UserControl? InfoPage { get => _infoPage; set => _infoPage = value ?? throw new NullReferenceException(); } 
        public virtual MainIcon MainIcon { get; }

        private Image? icon = Resources.red;
        private IConfiguration _configuration;
        private UserControl _infoPage = new InfoPage();

        protected IConfiguration? Configuration
        {
            get => _configuration;
            set => _configuration = value  ?? throw new ArgumentNullException("Mandatory parameter", nameof(IConfiguration));
        }

        public virtual Image Icon
        {
            get => icon ?? Resources.red;
            set
            {
                if (value == null) return;

                icon = value;
                if (MainIcon != null)
                {
                    MainIcon.Icon = value;
                    if (_infoPage != null)
                    {
                        if (_infoPage is InfoPage x) x.Icon = value; // Update InfoPage icon if it exists
                    }
                }
            }
        }

        protected BroadcastPlugin()
        {
          MainIcon = new MainIcon( this , Icon);
          if( _infoPage is InfoPage x ) x.Icon = Icon; // Set the initial icon for InfoPage
          Name = "Base Plugin";
          Description = "This is a base plugin for the Broadcast system.";
          Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0";
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
            var ConfigurationBuilder = new ConfigurationBuilder();
            ConfigurationBuilder.AddConfiguration(configSection);
            Configuration = ConfigurationBuilder.Build();
            return true;
        }
        public virtual void Start() 
        {
            // Default implementation does nothing, can be overridden by derived classes
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
        BroadcastPlugin parent;
        public MainIcon( BroadcastPlugin parent , Image Icon) : base()
        {
            this.parent = parent;
            this.BackgroundImage = Icon ?? Properties.Resources.red;
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
            parent.OnClick();
        }
        internal virtual void  OnMouseHover(object? sender, EventArgs e)
        {
            parent.OnHover();
        }
    }
}
