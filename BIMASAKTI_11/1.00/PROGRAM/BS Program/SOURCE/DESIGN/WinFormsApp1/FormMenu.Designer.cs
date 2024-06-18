namespace WinFormsApp1
{
    partial class Form1
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
        private void InitializeComponent()
        {
            PMR00100 = new Button();
            SuspendLayout();
            // 
            // PMR00100
            // 
            PMR00100.Location = new Point(11, 12);
            PMR00100.Name = "PMR00100";
            PMR00100.Size = new Size(75, 23);
            PMR00100.TabIndex = 0;
            PMR00100.Text = "PMR00100";
            PMR00100.UseVisualStyleBackColor = true;
            PMR00100.Click += PMR00100_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(288, 183);
            Controls.Add(PMR00100);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button PMR00100;
    }
}