namespace BroadcastPluginSDK;

public class MainIcon : PictureBox
{
    public Image Icon { get => this.BackgroundImage ?? Properties.Resources.red; set => this.BackgroundImage = value; }
    private readonly BroadcastPluginBase _parent;
    public MainIcon(BroadcastPluginBase parent, Image icon) : base()
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
    internal virtual void OnMouseHover(object? sender, EventArgs e)
    {
        _parent.OnHover();
    }
}

