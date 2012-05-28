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
            this.components = new System.ComponentModel.Container();
            this.SaveButton = new System.Windows.Forms.Button();
            this.EmaiTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.AddActionButton = new System.Windows.Forms.Button();
            this.ChartButton = new System.Windows.Forms.Button();
            this.RemoveActionButton = new System.Windows.Forms.Button();
            this.ActionsList = new System.Windows.Forms.ListView();
            this.userActionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.userActionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.SaveButton.Image = global::LifeLogger.Properties.Resources._005_Task_24x24_72;
            this.SaveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SaveButton.Location = new System.Drawing.Point(255, 239);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(62, 32);
            this.SaveButton.TabIndex = 0;
            this.SaveButton.Text = "Save";
            this.SaveButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SaveButton.UseVisualStyleBackColor = true;
            // 
            // EmaiTextBox
            // 
            this.EmaiTextBox.Location = new System.Drawing.Point(53, 22);
            this.EmaiTextBox.Name = "EmaiTextBox";
            this.EmaiTextBox.Size = new System.Drawing.Size(137, 20);
            this.EmaiTextBox.TabIndex = 1;
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(255, 22);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '*';
            this.PasswordTextBox.Size = new System.Drawing.Size(100, 20);
            this.PasswordTextBox.TabIndex = 2;
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
            this.CancelButton.BackgroundImage = global::LifeLogger.Properties.Resources._305_Close_24x24_72;
            this.CancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.CancelButton.Location = new System.Drawing.Point(323, 239);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(32, 32);
            this.CancelButton.TabIndex = 5;
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // AddActionButton
            // 
            this.AddActionButton.BackgroundImage = global::LifeLogger.Properties.Resources._112_Plus_Green_24x24_72;
            this.AddActionButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.AddActionButton.Location = new System.Drawing.Point(12, 239);
            this.AddActionButton.Name = "AddActionButton";
            this.AddActionButton.Size = new System.Drawing.Size(32, 32);
            this.AddActionButton.TabIndex = 7;
            this.AddActionButton.UseVisualStyleBackColor = true;
            // 
            // ChartButton
            // 
            this.ChartButton.BackgroundImage = global::LifeLogger.Properties.Resources.base_charts;
            this.ChartButton.Location = new System.Drawing.Point(88, 239);
            this.ChartButton.Name = "ChartButton";
            this.ChartButton.Size = new System.Drawing.Size(32, 32);
            this.ChartButton.TabIndex = 9;
            this.ChartButton.UseVisualStyleBackColor = true;
            // 
            // RemoveActionButton
            // 
            this.RemoveActionButton.BackgroundImage = global::LifeLogger.Properties.Resources._112_Minus_Orange_24x24_72;
            this.RemoveActionButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.RemoveActionButton.Location = new System.Drawing.Point(50, 239);
            this.RemoveActionButton.Name = "RemoveActionButton";
            this.RemoveActionButton.Size = new System.Drawing.Size(32, 32);
            this.RemoveActionButton.TabIndex = 10;
            this.RemoveActionButton.UseVisualStyleBackColor = true;
            // 
            // ActionsList
            // 
            this.ActionsList.Location = new System.Drawing.Point(12, 48);
            this.ActionsList.Name = "ActionsList";
            this.ActionsList.Size = new System.Drawing.Size(343, 185);
            this.ActionsList.TabIndex = 11;
            this.ActionsList.UseCompatibleStateImageBehavior = false;
            // 
            // userActionBindingSource
            // 
            this.userActionBindingSource.DataSource = typeof(LifeLogger.Settings.UserAction);
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 281);
            this.ControlBox = false;
            this.Controls.Add(this.ActionsList);
            this.Controls.Add(this.RemoveActionButton);
            this.Controls.Add(this.ChartButton);
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
            ((System.ComponentModel.ISupportInitialize)(this.userActionBindingSource)).EndInit();
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
        internal System.Windows.Forms.Button RemoveActionButton;
        internal System.Windows.Forms.Button ChartButton;
        private System.Windows.Forms.BindingSource userActionBindingSource;
        internal System.Windows.Forms.ListView ActionsList;
    }
}