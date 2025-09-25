namespace APDesign;

partial class FormMenu
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
        APR00500_Button = new Button();
        APR00300_Button = new Button();
        SuspendLayout();
        // 
        // APR00500_Button
        // 
        APR00500_Button.Location = new Point(12, 37);
        APR00500_Button.Name = "APR00500_Button";
        APR00500_Button.Size = new Size(75, 23);
        APR00500_Button.TabIndex = 0;
        APR00500_Button.Text = "APR00500";
        APR00500_Button.UseVisualStyleBackColor = true;
        APR00500_Button.Click += APR00500_Button_Click;
        // 
        // APR00300_Button
        // 
        APR00300_Button.Location = new Point(12, 8);
        APR00300_Button.Name = "APR00300_Button";
        APR00300_Button.Size = new Size(75, 23);
        APR00300_Button.TabIndex = 1;
        APR00300_Button.Text = "APR00300";
        APR00300_Button.UseVisualStyleBackColor = true;
        APR00300_Button.Click += APR00300_Button_Click;
        // 
        // FormMenu
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(270, 99);
        Controls.Add(APR00300_Button);
        Controls.Add(APR00500_Button);
        Name = "FormMenu";
        Text = "Form Menu AP Report";
        Load += FormMenu_Load;
        ResumeLayout(false);
    }

    #endregion

    private Button APR00500_Button;
    private Button APR00300_Button;
}