using System.Windows.Forms;

namespace WinFormUserInterface
{
    public partial class FormGameSettings : Form
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
            this.StartPosition = FormStartPosition.CenterScreen;
            this.label_BoardSize = new System.Windows.Forms.Label();
            this.label_Players = new System.Windows.Forms.Label();
            this.label_Player1 = new System.Windows.Forms.Label();
            this.radioButton_size6 = new System.Windows.Forms.RadioButton();
            this.radioButton_size8 = new System.Windows.Forms.RadioButton();
            this.radioButton_size10 = new System.Windows.Forms.RadioButton();
            this.checkBox_Player2 = new System.Windows.Forms.CheckBox();
            this.textBox_Player1 = new System.Windows.Forms.TextBox();
            this.textBox_Player2 = new System.Windows.Forms.TextBox();
            this.button_Done = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_BoardSize
            // 
            this.label_BoardSize.AutoSize = true;
            this.label_BoardSize.Location = new System.Drawing.Point(12, 14);
            this.label_BoardSize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_BoardSize.Name = "label_BoardSize";
            this.label_BoardSize.Size = new System.Drawing.Size(61, 13);
            this.label_BoardSize.TabIndex = 0;
            this.label_BoardSize.Text = "Board Size:";
            // 
            // label_Players
            // 
            this.label_Players.AutoSize = true;
            this.label_Players.Location = new System.Drawing.Point(12, 56);
            this.label_Players.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Players.Name = "label_Players";
            this.label_Players.Size = new System.Drawing.Size(44, 13);
            this.label_Players.TabIndex = 1;
            this.label_Players.Text = "Players:";
            // 
            // label_Player1
            // 
            this.label_Player1.AutoSize = true;
            this.label_Player1.Location = new System.Drawing.Point(12, 75);
            this.label_Player1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Player1.Name = "label_Player1";
            this.label_Player1.Size = new System.Drawing.Size(48, 13);
            this.label_Player1.TabIndex = 3;
            this.label_Player1.Text = "Player 1:";
            // 
            // radioButton_size6
            // 
            this.radioButton_size6.AutoSize = true;
            this.radioButton_size6.Location = new System.Drawing.Point(27, 29);
            this.radioButton_size6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton_size6.Name = "radioButton_size6";
            this.radioButton_size6.Size = new System.Drawing.Size(48, 17);
            this.radioButton_size6.TabIndex = 4;
            this.radioButton_size6.TabStop = true;
            this.radioButton_size6.Text = "6 x 6";
            this.radioButton_size6.UseVisualStyleBackColor = true;
            // 
            // radioButton_size8
            // 
            this.radioButton_size8.AutoSize = true;
            this.radioButton_size8.Location = new System.Drawing.Point(75, 29);
            this.radioButton_size8.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton_size8.Name = "radioButton_size8";
            this.radioButton_size8.Size = new System.Drawing.Size(48, 17);
            this.radioButton_size8.TabIndex = 5;
            this.radioButton_size8.TabStop = true;
            this.radioButton_size8.Text = "8 x 8";
            this.radioButton_size8.UseVisualStyleBackColor = true;
            // 
            // radioButton_size10
            // 
            this.radioButton_size10.AutoSize = true;
            this.radioButton_size10.Location = new System.Drawing.Point(124, 29);
            this.radioButton_size10.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton_size10.Name = "radioButton_size10";
            this.radioButton_size10.Size = new System.Drawing.Size(60, 17);
            this.radioButton_size10.TabIndex = 6;
            this.radioButton_size10.TabStop = true;
            this.radioButton_size10.Text = "10 x 10";
            this.radioButton_size10.UseVisualStyleBackColor = true;
            // 
            // checkBox_Player2
            // 
            this.checkBox_Player2.AutoSize = true;
            this.checkBox_Player2.Location = new System.Drawing.Point(15, 99);
            this.checkBox_Player2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox_Player2.Name = "checkBox_Player2";
            this.checkBox_Player2.Size = new System.Drawing.Size(67, 17);
            this.checkBox_Player2.TabIndex = 7;
            this.checkBox_Player2.Text = "Player 2:";
            this.checkBox_Player2.UseVisualStyleBackColor = true;
    //        this.checkBox_Player2.CheckedChanged += new System.EventHandler(this.checkBox_Player2_CheckedChanged);
            this.checkBox_Player2.Click += new System.EventHandler(this.button_Click);
            // 
            // textBox_Player1
            // 
            this.textBox_Player1.Location = new System.Drawing.Point(93, 75);
            this.textBox_Player1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_Player1.Name = "textBox_Player1";
            this.textBox_Player1.Size = new System.Drawing.Size(83, 20);
            this.textBox_Player1.TabIndex = 8;
            // 
            // textBox_Player2
            // 
            this.textBox_Player2.Enabled = false;
            this.textBox_Player2.Location = new System.Drawing.Point(93, 97);
            this.textBox_Player2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_Player2.Name = "textBox_Player2";
            this.textBox_Player2.Size = new System.Drawing.Size(83, 20);
            this.textBox_Player2.TabIndex = 9;
            this.textBox_Player2.Text = "Computer";
            // 
            // button_Done
            // 
            this.button_Done.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_Done.Location = new System.Drawing.Point(113, 126);
            this.button_Done.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button_Done.Name = "button_Done";
            this.button_Done.Size = new System.Drawing.Size(61, 23);
            this.button_Done.TabIndex = 10;
            this.button_Done.Text = "Done";
            this.button_Done.UseVisualStyleBackColor = true;
            this.button_Done.Click += new System.EventHandler(this.button_Click);
            // 
            // FormGameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(191, 159);
            this.Controls.Add(this.button_Done);
            this.Controls.Add(this.textBox_Player2);
            this.Controls.Add(this.textBox_Player1);
            this.Controls.Add(this.checkBox_Player2);
            this.Controls.Add(this.radioButton_size10);
            this.Controls.Add(this.radioButton_size8);
            this.Controls.Add(this.radioButton_size6);
            this.Controls.Add(this.label_Player1);
            this.Controls.Add(this.label_Players);
            this.Controls.Add(this.label_BoardSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormGameSettings";
            this.Text = "Game Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label_BoardSize;
        private Label label_Players;
        private Label label_Player1;
        private RadioButton radioButton_size6;
        private RadioButton radioButton_size8;
        private RadioButton radioButton_size10;
        private CheckBox checkBox_Player2;
        private TextBox textBox_Player1;
        private TextBox textBox_Player2;
        private Button button_Done;
    }
}