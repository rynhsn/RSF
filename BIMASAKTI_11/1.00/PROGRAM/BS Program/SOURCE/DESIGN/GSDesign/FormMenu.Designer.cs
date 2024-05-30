namespace GSDesign
{
    partial class FormMenu
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            BaseHeaderLandscape = new Button();
            BaseHeader = new Button();
            GLI00100 = new Button();
            GLR00100_3 = new Button();
            SuspendLayout();
            // 
            // BaseHeaderLandscape
            // 
            BaseHeaderLandscape.Location = new Point(302, 12);
            BaseHeaderLandscape.Name = "BaseHeaderLandscape";
            BaseHeaderLandscape.Size = new Size(179, 23);
            BaseHeaderLandscape.TabIndex = 1;
            BaseHeaderLandscape.Text = "Base Header Landscape";
            BaseHeaderLandscape.UseVisualStyleBackColor = true;
            BaseHeaderLandscape.Click += BaseHeaderLandscape_Click;
            // 
            // BaseHeader
            // 
            BaseHeader.Location = new Point(302, 41);
            BaseHeader.Name = "BaseHeader";
            BaseHeader.Size = new Size(179, 23);
            BaseHeader.TabIndex = 2;
            BaseHeader.Text = "Base Header";
            BaseHeader.UseVisualStyleBackColor = true;
            BaseHeader.Click += BaseHeader_Click;
            // 
            // GLI00100
            // 
            GLI00100.Location = new Point(12, 12);
            GLI00100.Name = "GLI00100";
            GLI00100.Size = new Size(179, 23);
            GLI00100.TabIndex = 3;
            GLI00100.Text = "GLI00100 - Account Status";
            GLI00100.UseVisualStyleBackColor = true;
            GLI00100.Click += GLI00100_Click;
            // 
            // GLR00100_3
            // 
            GLR00100_3.Location = new Point(12, 41);
            GLR00100_3.Name = "GLR00100_3";
            GLR00100_3.Size = new Size(179, 39);
            GLR00100_3.TabIndex = 4;
            GLR00100_3.Text = "GLR00100 - Activity Report (Based On Date)";
            GLR00100_3.UseVisualStyleBackColor = true;
            GLR00100_3.Click += GLR00100_3_Click;
            // 
            // FormMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(493, 144);
            Controls.Add(GLR00100_3);
            Controls.Add(GLI00100);
            Controls.Add(BaseHeader);
            Controls.Add(BaseHeaderLandscape);
            Name = "FormMenu";
            Text = "FormMenu";
            Load += FormMenu_Load;
            ResumeLayout(false);
        }

        #endregion
        private Button BaseHeaderLandscape;
        private Button BaseHeader;
        private Button GLI00100;
        private Button GLR00100_3;
    }
}