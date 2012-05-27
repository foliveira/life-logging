using System;
using System.ComponentModel;
using System.Windows.Forms.DataVisualization.Charting;

namespace LifeLogger.UI.Mediators
{
    public class ChartMediator : AbstractLoggerMediator<ChartWindow>
    {
        public override void RegisterEvents()
        {
            MediatingForm.SaveButton.Click += OnSaveButtonEvent;
            MediatingForm.CloseButton.Click += OnCloseButtonEvent;

            MediatingForm.SaveDialog.FileOk += OnFileOkEvent;
        }

        private void OnFileOkEvent(object sender, CancelEventArgs e)
        {
            MediatingForm.ActionsChart.SaveImage(MediatingForm.SaveDialog.FileName, ChartImageFormat.Png);
        }

        private void OnSaveButtonEvent(object sender, EventArgs e)
        {
            MediatingForm.SaveDialog.ShowDialog(MediatingForm);
        }

        private void OnCloseButtonEvent(object sender, EventArgs e)
        {
            MediatingForm.Hide();
        }
    }
}
