using BroadcastPluginSDK.abstracts;
using BroadcastPluginSDK.Properties;

namespace BroadcastPluginSDK.Classes;

public class MainIcon : PictureBox
{
    private readonly BroadcastPluginBase _parent;

    public MainIcon(BroadcastPluginBase parent, Image icon)
    {
        _parent = parent;
        BackgroundImage = icon ?? Resources.red;
        BackgroundImageLayout = ImageLayout.Stretch;
        Size = new Size(100, 100);
        Name = "API";
        TabIndex = 0;
        TabStop = false;

        Click += OnClick;
        MouseHover += OnMouseHover;
    }

    public Image Icon
    {
        get => BackgroundImage ?? Resources.red;
        set => BackgroundImage = value;
    }

    internal virtual void OnClick(object? sender, EventArgs e)
    {
        _parent.OnClick();
    }

    internal virtual void OnMouseHover(object? sender, EventArgs e)
    {
        _parent.OnHover();
    }
}