namespace PMA00300DESIGN
{
    partial class Design
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        /// 
        private Button buttonPMA00300;
        private void InitializeComponent()
        {
            buttonPMA00300 = new Button();
            SuspendLayout();
            // 
            // buttonPMA00300
            // 
            buttonPMA00300.Location = new Point(172, 27);
            buttonPMA00300.Margin = new Padding(3, 2, 3, 2);
            buttonPMA00300.Name = "buttonPMA00300";
            buttonPMA00300.Size = new Size(114, 27);
            buttonPMA00300.TabIndex = 0;
            buttonPMA00300.Text = "buttonPMA00300";
            buttonPMA00300.TextAlign = ContentAlignment.TopCenter;
            buttonPMA00300.Click += buttonPMA00300_Click;
            // 
            // Design
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonPMA00300);
            Name = "Design";
            Text = "DesignForm";
            Load += Design_Load;
            ResumeLayout(false);
        }
        #endregion
    }
}