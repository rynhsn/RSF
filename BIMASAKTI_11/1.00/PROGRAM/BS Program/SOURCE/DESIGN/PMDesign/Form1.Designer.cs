namespace PMDesign
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
            button1 = new Button();
            PMR02600Button = new Button();
            PMR00460 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "PMR00400";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // PMR02600Button
            // 
            PMR02600Button.Location = new Point(15, 44);
            PMR02600Button.Name = "PMR02600Button";
            PMR02600Button.Size = new Size(75, 23);
            PMR02600Button.TabIndex = 1;
            PMR02600Button.Text = "PMR02600";
            PMR02600Button.UseVisualStyleBackColor = true;
            PMR02600Button.Click += PMR02600Button_Click;
            // 
            // PMR00460
            // 
            PMR00460.Location = new Point(17, 74);
            PMR00460.Name = "PMR00460";
            PMR00460.Size = new Size(75, 23);
            PMR00460.TabIndex = 2;
            PMR00460.Text = "PMR00460";
            PMR00460.UseVisualStyleBackColor = true;
            PMR00460.Click += PMR00460_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(318, 212);
            Controls.Add(PMR00460);
            Controls.Add(PMR02600Button);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button PMR02600Button;
        private Button PMR00460;
    }
}