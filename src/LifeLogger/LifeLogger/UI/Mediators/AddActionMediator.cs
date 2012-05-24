namespace LifeLogger.UI.Mediators
{
    using System;

    internal class AddActionMediator : AbstractLoggerMediator<AddActionWindow>
    {
        public override void RegisterEvents()
        {
            MediatingForm.AddButton.Click += OnAddButtonEvent;
            MediatingForm.CancelButton.Click += OnCancelButtonEvent;
        }

        private void OnAddButtonEvent(object sender, EventArgs e)
        {
            var ua = Program.Settings.AddAction(MediatingForm.ActionTextBox.Text);

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
