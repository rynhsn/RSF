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
            PMR03400 = new Button();
            PMR00800 = new Button();
            button3 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(14, 16);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(86, 30);
            button1.TabIndex = 0;
            button1.Text = "PMR00400";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // PMR02600Button
            // 
            PMR02600Button.Location = new Point(17, 58);
            PMR02600Button.Margin = new Padding(3, 4, 3, 4);
            PMR02600Button.Name = "PMR02600Button";
            PMR02600Button.Size = new Size(86, 30);
            PMR02600Button.TabIndex = 1;
            PMR02600Button.Text = "PMR02600";
            PMR02600Button.UseVisualStyleBackColor = true;
            PMR02600Button.Click += PMR02600Button_Click;
            // 
            // PMR00460
            // 
            PMR00460.Location = new Point(19, 98);
            PMR00460.Margin = new Padding(3, 4, 3, 4);
            PMR00460.Name = "PMR00460";
            PMR00460.Size = new Size(86, 30);
            PMR00460.TabIndex = 2;
            PMR00460.Text = "PMR00460";
            PMR00460.UseVisualStyleBackColor = true;
            PMR00460.Click += PMR00460_Click;
            // 
            // BTN_PMR03300
            // 
            BTN_PMR03300.Location = new Point(19, 135);
            BTN_PMR03300.Margin = new Padding(2);
            BTN_PMR03300.Name = "BTN_PMR03300";
            BTN_PMR03300.Size = new Size(90, 27);
            BTN_PMR03300.TabIndex = 3;
            BTN_PMR03300.Text = "PMR03300";
            BTN_PMR03300.UseVisualStyleBackColor = true;
            BTN_PMR03300.Click += BTN_PMR03300_Click;
            // 
            // PMR03400
            // 
            PMR03400.Location = new Point(132, 58);
            PMR03400.Margin = new Padding(2);
            PMR03400.Name = "PMR03400";
            PMR03400.Size = new Size(170, 67);
            PMR03400.TabIndex = 3;
            PMR03400.Text = "PMR03400";
            PMR03400.UseVisualStyleBackColor = true;
            PMR03400.Click += PMR03400_Click;
            // 
            // PMR00800
            // 
            PMR00800.Location = new Point(158, 19);
            PMR00800.Margin = new Padding(2);
            PMR00800.Name = "PMR00800";
            PMR00800.Size = new Size(90, 27);
            PMR00800.TabIndex = 4;
            PMR00800.Text = "PMR00800";
            PMR00800.UseVisualStyleBackColor = true;
            PMR00800.Click += PMR00800_Click;
            // 
            // button3
            // 
            button3.Location = new Point(330, 142);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 5;
            button3.Text = "pmt01700";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(562, 324);
            Controls.Add(button3);
            Controls.Add(PMR00800);
            Controls.Add(BTN_PMR03300);
            Controls.Add(PMR03400);
            Controls.Add(PMR00460);
            Controls.Add(PMR02600Button);
            Controls.Add(button1);
            Margin = new Padding(3, 4, 3, 4);
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
        private Button PMR03400;
        private Button PMR00800;
        private Button button2;
        private Button button3;
    }
}