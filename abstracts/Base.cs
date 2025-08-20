using System.Diagnostics;
using System.Reflection;
using BroadcastPluginSDK.Classes;
using BroadcastPluginSDK.Interfaces;
using BroadcastPluginSDK.Properties;
using Microsoft.Extensions.Configuration;

namespace BroadcastPluginSDK.abstracts;

public abstract class BroadcastPluginBase : IPlugin
{
    private IConfiguration _configuration;

     
    private string ?_name;
    private Image? _icon;
    private IInfoPage? _infoPage;
    private MainIcon _mainIcon;
    private string _stanza;
    private static Image DefaultImage => Resources.red;
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
        _name = name ?? GetAssemblyMetadata("Name") ?? "Unknown Plugin";
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


    public virtual Image Icon
    {
        get => _icon ?? Resources.red;
        set => _icon = value;
    }

    public Assembly DerivedAssembly => GetType().Assembly;

    public string Name
    {
        get => _name ?? GetAssemblyMetadata("Name") ?? "Unknown Plugin";
        set => _name = value;
    }

    public string Version => GetAssemblyMetadata("Version") ?? "0.0.0";
    public string Description => GetAssemblyMetadata("Description") ?? "No description available.";

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
        get => _infoPage ?? new InfoPage();
        set => _infoPage = value as InfoPage;
    }

    public virtual GetCacheDataDelegate? GetCacheData { set; get; } = null;
    public string FilePath { get; set; } = string.Empty;
    public string RepositoryUrl => GetAssemblyMetadata("RepositoryUrl") ?? string.Empty;

    public event EventHandler? Click;
    public event EventHandler? MouseHover;

    private string? GetAssemblyMetadata(string key)
    {
        return DerivedAssembly
            .GetCustomAttributes<AssemblyMetadataAttribute>()
            .FirstOrDefault(attr => attr.Key == key)
            ?.Value;
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

    internal void OnClick()
    {
        Click?.Invoke(this, EventArgs.Empty);
    }

    internal void OnHover()
    {
        MouseHover?.Invoke(this, EventArgs.Empty);
    }
}