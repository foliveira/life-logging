namespace LifeLogger.UI
{
    partial class ChartWindow
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.ActionsChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.SaveDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.ActionsChart)).BeginInit();
            this.SuspendLayout();
            // 
            // ActionsChart
            // 
            chartArea3.Name = "ChartArea1";
            this.ActionsChart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.ActionsChart.Legends.Add(legend3);
            this.ActionsChart.Location = new System.Drawing.Point(12, 12);
            this.ActionsChart.Name = "ActionsChart";
            this.ActionsChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.ActionsChart.Series.Add(series3);
            this.ActionsChart.Size = new System.Drawing.Size(902, 302);
            this.ActionsChart.TabIndex = 0;
            this.ActionsChart.Text = "ActionsChart";
            // 
            // CloseButton
            // 
            this.CloseButton.BackgroundImage = global::LifeLogger.Properties.Resources._305_Close_24x24_72;
            this.CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.CloseButton.Location = new System.Drawing.Point(882, 320);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(32, 32);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            this.SaveButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.SaveButton.Image = global::LifeLogger.Properties.Resources.GenVideoDoc_32x32_72;
            this.SaveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SaveButton.Location = new System.Drawing.Point(769, 320);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(107, 32);
            this.SaveButton.TabIndex = 2;
            this.SaveButton.Text = "Save to file...";
            this.SaveButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SaveButton.UseVisualStyleBackColor = true;
            // 
            // SaveDialog
            // 
            this.SaveDialog.DefaultExt = "png";
            this.SaveDialog.FileName = "logger_chart";
            this.SaveDialog.Filter = "PNG File|*.png";
            // 
            // ChartWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 357);
            this.ControlBox = false;
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.ActionsChart);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ChartWindow";
            this.Text = "ChartWindow";
            ((System.ComponentModel.ISupportInitialize)(this.ActionsChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DataVisualization.Charting.Chart ActionsChart;
        internal System.Windows.Forms.Button CloseButton;
        internal System.Windows.Forms.Button SaveButton;
        internal System.Windows.Forms.SaveFileDialog SaveDialog;


    }
}