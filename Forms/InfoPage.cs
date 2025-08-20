using BroadcastPluginSDK.Interfaces;
using BroadcastPluginSDK.Properties;

namespace BroadcastPluginSDK;

public partial class InfoPage : UserControl, IInfoPage
{
    public InfoPage()
    {
        InitializeComponent();
    }

    public Image? Icon
    {
        set
        {
            if (pictureBox1 is null) return;
            pictureBox1.BackgroundImage = value ?? Resources.red;
        }
    }

    public new string Name
    {
        set => pName.Text = value;
        get => pName.Text;
    }

    public string Version
    {
        set => pVersion.Text = value;
        get => pVersion.Text;
    }

    public string Description
    {
        set => pDescription.Text = value;
        get => pDescription.Text;
    }

    public Control GetControl()
    {
        return this;
    }

    private void pName_Click(object sender, EventArgs e)
    {
    }
}