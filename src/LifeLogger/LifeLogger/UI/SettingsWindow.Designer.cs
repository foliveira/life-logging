using System.ComponentModel;
using LifeLogger.Settings;

namespace LifeLogger.UI
{
    partial class SettingsWindow
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
            this.SaveButton = new System.Windows.Forms.Button();
            this.EmaiTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.AddActionButton = new System.Windows.Forms.Button();
            this.ActionsList = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(204, 239);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 0;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            // 
            // EmaiTextBox
            // 
            this.EmaiTextBox.Location = new System.Drawing.Point(53, 22);
            this.EmaiTextBox.Name = "EmaiTextBox";
            this.EmaiTextBox.Size = new System.Drawing.Size(137, 20);
            this.EmaiTextBox.TabIndex = 1;
            this.EmaiTextBox.Text = "lifelogger2012@gmail.com";
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(255, 22);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '*';
            this.PasswordTextBox.Size = new System.Drawing.Size(100, 20);
            this.PasswordTextBox.TabIndex = 2;
            this.PasswordTextBox.Text = "fabiogvipjoao";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "E-mail";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password";
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(285, 239);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 5;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // AddActionButton
            // 
            this.AddActionButton.Location = new System.Drawing.Point(15, 239);
            this.AddActionButton.Name = "AddActionButton";
            this.AddActionButton.Size = new System.Drawing.Size(75, 23);
            this.AddActionButton.TabIndex = 7;
            this.AddActionButton.Text = "New Action";
            this.AddActionButton.UseVisualStyleBackColor = true;
            // 
            // ActionsList
            // 
            this.ActionsList.Location = new System.Drawing.Point(12, 77);
            this.ActionsList.Name = "ActionsList";
            this.ActionsList.Size = new System.Drawing.Size(348, 156);
            this.ActionsList.TabIndex = 8;
            this.ActionsList.UseCompatibleStateImageBehavior = false;
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 274);
            this.Controls.Add(this.ActionsList);
            this.Controls.Add(this.AddActionButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.EmaiTextBox);
            this.Controls.Add(this.SaveButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Button SaveButton;
        internal new System.Windows.Forms.Button CancelButton;
        internal System.Windows.Forms.Button AddActionButton;
        internal System.Windows.Forms.TextBox EmaiTextBox;
        internal System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn actionNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn shortcutsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hintDataGridViewTextBoxColumn;
        internal System.Windows.Forms.ListView ActionsList;
    }
}