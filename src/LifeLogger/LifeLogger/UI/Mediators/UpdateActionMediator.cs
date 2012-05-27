namespace LifeLogger.UI.Mediators
{
    using System;
    using System.Linq;

    internal class UpdateActionMediator : AbstractLoggerMediator<UpdateActionWindow>
    {
        public override void RegisterEvents()
        {
            MediatingForm.UpdateButton.Click += OnUpdateButtonEvent;
            MediatingForm.CancelButton.Click += OnCancelButtonEvent;
        }

        private void OnUpdateButtonEvent(object sender, EventArgs e)
        {
            var ua = Program.Settings.Actions.First(u => u.ActionName.Equals(MediatingForm.ActionTextBox.Text));

            ua.Shortcuts = MediatingForm.KeyWordsTextBox.Text;
            ua.Hint = MediatingForm.HintTextBox.Text;

            MediatingForm.ActionTextBox.Text = String.Empty;
            MediatingForm.KeyWordsTextBox.Text = String.Empty;
            MediatingForm.HintTextBox.Text = String.Empty;

            MediatingForm.Hide();
        }

        private void OnCancelButtonEvent(object sender, EventArgs e)
        {
            MediatingForm.Hide();
        }
    }
}
