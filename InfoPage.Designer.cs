namespace BroadcastPluginSDK
{
    partial class InfoPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            pName = new Label();
            pVersion = new Label();
            pDescription = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.green;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(79, 15);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(272, 250);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pName
            // 
            pName.AutoSize = true;
            pName.Location = new Point(79, 277);
            pName.MinimumSize = new Size(272, 0);
            pName.Name = "pName";
            pName.Size = new Size(272, 15);
            pName.TabIndex = 1;
            pName.Text = "pName";
            pName.TextAlign = ContentAlignment.MiddleCenter;
            pName.Click += pName_Click;
            // 
            // pVersion
            // 
            pVersion.AutoSize = true;
            pVersion.Location = new Point(79, 301);
            pVersion.MinimumSize = new Size(272, 0);
            pVersion.Name = "pVersion";
            pVersion.Size = new Size(272, 15);
            pVersion.TabIndex = 2;
            pVersion.Text = "pVersion";
            pVersion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pDescription
            // 
            pDescription.BorderStyle = BorderStyle.None;
            pDescription.Enabled = false;
            pDescription.Location = new Point(79, 319);
            pDescription.Multiline = true;
            pDescription.Name = "pDescription";
            pDescription.Size = new Size(272, 47);
            pDescription.TabIndex = 3;
            pDescription.TextAlign = HorizontalAlignment.Center;
            // 
            // InfoPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pDescription);
            Controls.Add(pVersion);
            Controls.Add(pName);
            Controls.Add(pictureBox1);
            Name = "InfoPage";
            Size = new Size(448, 450);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private Label pName;
        private Label pVersion;
        private TextBox pDescription;
    }
}
