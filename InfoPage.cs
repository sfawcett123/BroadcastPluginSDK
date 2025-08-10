using PluginBase.Properties;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Forms;

namespace PluginBase
{
    public partial class InfoPage : UserControl 
    {
        public Image? Icon
        {
            set
            {
                if (pictureBox1 is null)
                {
                    return;
                }
                pictureBox1.BackgroundImage = value ??  Resources.red ;
            }
        }
        public new string Name { set => pName.Text = value; get => pName.Text; }
        public string Version { set => pVersion.Text = value; get => pVersion.Text; }
        public string Description { set => pDescription.Text = value; get => pDescription.Text; }

        public InfoPage()
        {
            InitializeComponent();
        }

        private void pName_Click(object sender, EventArgs e)
        {

        }
    }
}
