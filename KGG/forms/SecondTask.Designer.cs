using System.Drawing;
using System.Windows.Forms;

namespace KGG
{
    partial class SecondTask
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
            this.Panel = new System.Windows.Forms.Panel();
            this.coefficientATextBox = new System.Windows.Forms.TextBox();
            this.coefficientBTextBox = new System.Windows.Forms.TextBox();
            this.coefficientCTextBox = new System.Windows.Forms.TextBox();
            this.leftBorderTextBox = new System.Windows.Forms.TextBox();
            this.rightBorderTextBox = new System.Windows.Forms.TextBox();
            this.coefficientALabel = new System.Windows.Forms.Label();
            this.coefficientBLabel = new System.Windows.Forms.Label();
            this.coefficientCLabel = new System.Windows.Forms.Label();
            this.leftBorderLabel = new System.Windows.Forms.Label();
            this.rightBorderLabel = new System.Windows.Forms.Label();
            this.drawButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Panel
            // 
            this.Panel.Location = new System.Drawing.Point(12, 12);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(500, 500);
            this.Panel.TabIndex = 0;
            // 
            // coefficientATextBox
            // 
            this.coefficientATextBox.Location = new System.Drawing.Point(607, 45);
            this.coefficientATextBox.Name = "coefficientATextBox";
            this.coefficientATextBox.Size = new System.Drawing.Size(100, 22);
            this.coefficientATextBox.TabIndex = 1;
            this.coefficientATextBox.Text = "1";
            // 
            // coefficientBTextBox
            // 
            this.coefficientBTextBox.Location = new System.Drawing.Point(607, 76);
            this.coefficientBTextBox.Name = "coefficientBTextBox";
            this.coefficientBTextBox.Size = new System.Drawing.Size(100, 22);
            this.coefficientBTextBox.TabIndex = 2;
            this.coefficientBTextBox.Text = "1";
            // 
            // coefficientCTextBox
            // 
            this.coefficientCTextBox.Location = new System.Drawing.Point(607, 104);
            this.coefficientCTextBox.Name = "coefficientCTextBox";
            this.coefficientCTextBox.Size = new System.Drawing.Size(100, 22);
            this.coefficientCTextBox.TabIndex = 3;
            this.coefficientCTextBox.Text = "1";
            // 
            // leftBorderTextBox
            // 
            this.leftBorderTextBox.Location = new System.Drawing.Point(608, 132);
            this.leftBorderTextBox.Name = "leftBorderTextBox";
            this.leftBorderTextBox.Size = new System.Drawing.Size(100, 22);
            this.leftBorderTextBox.TabIndex = 4;
            this.leftBorderTextBox.Text = "0";
            // 
            // rightBorderTextBox
            // 
            this.rightBorderTextBox.Location = new System.Drawing.Point(607, 160);
            this.rightBorderTextBox.Name = "rightBorderTextBox";
            this.rightBorderTextBox.Size = new System.Drawing.Size(100, 22);
            this.rightBorderTextBox.TabIndex = 5;
            this.rightBorderTextBox.Text = "360";
            // 
            // coefficientALabel
            // 
            this.coefficientALabel.AutoSize = true;
            this.coefficientALabel.Location = new System.Drawing.Point(581, 45);
            this.coefficientALabel.Name = "coefficientALabel";
            this.coefficientALabel.Size = new System.Drawing.Size(16, 17);
            this.coefficientALabel.TabIndex = 6;
            this.coefficientALabel.Text = "a";
            this.coefficientALabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // coefficientBLabel
            // 
            this.coefficientBLabel.AutoSize = true;
            this.coefficientBLabel.Location = new System.Drawing.Point(581, 76);
            this.coefficientBLabel.Name = "coefficientBLabel";
            this.coefficientBLabel.Size = new System.Drawing.Size(16, 17);
            this.coefficientBLabel.TabIndex = 9;
            this.coefficientBLabel.Text = "b";
            // 
            // coefficientCLabel
            // 
            this.coefficientCLabel.AutoSize = true;
            this.coefficientCLabel.Location = new System.Drawing.Point(582, 104);
            this.coefficientCLabel.Name = "coefficientCLabel";
            this.coefficientCLabel.Size = new System.Drawing.Size(15, 17);
            this.coefficientCLabel.TabIndex = 10;
            this.coefficientCLabel.Text = "c";
            // 
            // leftBorderLabel
            // 
            this.leftBorderLabel.AutoSize = true;
            this.leftBorderLabel.Location = new System.Drawing.Point(524, 135);
            this.leftBorderLabel.Name = "leftBorderLabel";
            this.leftBorderLabel.Size = new System.Drawing.Size(73, 17);
            this.leftBorderLabel.TabIndex = 11;
            this.leftBorderLabel.Text = "left border";
            // 
            // rightBorderLabel
            // 
            this.rightBorderLabel.AutoSize = true;
            this.rightBorderLabel.Location = new System.Drawing.Point(519, 163);
            this.rightBorderLabel.Name = "rightBorderLabel";
            this.rightBorderLabel.Size = new System.Drawing.Size(82, 17);
            this.rightBorderLabel.TabIndex = 13;
            this.rightBorderLabel.Text = "right border";
            // 
            // drawButton
            // 
            this.drawButton.Location = new System.Drawing.Point(632, 188);
            this.drawButton.Name = "drawButton";
            this.drawButton.Size = new System.Drawing.Size(75, 23);
            this.drawButton.TabIndex = 14;
            this.drawButton.Text = "Draw";
            this.drawButton.UseVisualStyleBackColor = true;
            this.drawButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // SecondTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1454, 674);
            this.Controls.Add(this.drawButton);
            this.Controls.Add(this.rightBorderLabel);
            this.Controls.Add(this.leftBorderLabel);
            this.Controls.Add(this.coefficientCLabel);
            this.Controls.Add(this.coefficientBLabel);
            this.Controls.Add(this.coefficientALabel);
            this.Controls.Add(this.rightBorderTextBox);
            this.Controls.Add(this.leftBorderTextBox);
            this.Controls.Add(this.coefficientCTextBox);
            this.Controls.Add(this.coefficientBTextBox);
            this.Controls.Add(this.coefficientATextBox);
            this.Controls.Add(this.Panel);
            this.Name = "SecondTask";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel Panel;
        private TextBox coefficientATextBox;
        private TextBox coefficientBTextBox;
        private TextBox coefficientCTextBox;
        private TextBox leftBorderTextBox;
        private TextBox rightBorderTextBox;
        private Label coefficientALabel;
        private Label coefficientBLabel;
        private Label coefficientCLabel;
        private Label leftBorderLabel;
        private Label rightBorderLabel;
        private Button drawButton;
    }
}

