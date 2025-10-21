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
            BTN_PMR03300 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(17, 20);
            button1.Margin = new Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new Size(107, 38);
            button1.TabIndex = 0;
            button1.Text = "PMR00400";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // PMR02600Button
            // 
            PMR02600Button.Location = new Point(21, 73);
            PMR02600Button.Margin = new Padding(4, 5, 4, 5);
            PMR02600Button.Name = "PMR02600Button";
            PMR02600Button.Size = new Size(107, 38);
            PMR02600Button.TabIndex = 1;
            PMR02600Button.Text = "PMR02600";
            PMR02600Button.UseVisualStyleBackColor = true;
            PMR02600Button.Click += PMR02600Button_Click;
            // 
            // PMR00460
            // 
            PMR00460.Location = new Point(24, 123);
            PMR00460.Margin = new Padding(4, 5, 4, 5);
            PMR00460.Name = "PMR00460";
            PMR00460.Size = new Size(107, 38);
            PMR00460.TabIndex = 2;
            PMR00460.Text = "PMR00460";
            PMR00460.UseVisualStyleBackColor = true;
            PMR00460.Click += PMR00460_Click;
            // 
            // BTN_PMR03300
            // 
            BTN_PMR03300.Location = new Point(24, 169);
            BTN_PMR03300.Name = "BTN_PMR03300";
            BTN_PMR03300.Size = new Size(112, 34);
            BTN_PMR03300.TabIndex = 3;
            BTN_PMR03300.Text = "PMR03300";
            BTN_PMR03300.UseVisualStyleBackColor = true;
            BTN_PMR03300.Click += BTN_PMR03300_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(454, 353);
            Controls.Add(BTN_PMR03300);
            Controls.Add(PMR00460);
            Controls.Add(PMR02600Button);
            Controls.Add(button1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button PMR02600Button;
        private Button PMR00460;
        private Button BTN_PMR03300;
    }
}