namespace LifeLogger.UI.Mediators
{
    using System;

    internal class SettingsMediator : AbstractLoggerMediator<SettingsWindow>
    {
        public override void RegisterEvents()
        {
            MediatingForm.SaveButton.Click += OnSaveButtonEvent;
            MediatingForm.CancelButton.Click += OnCancelButtonEvent;
            MediatingForm.AddActionButton.Click += OnAddActionButtonEvent;
            MediatingForm.ActionsList.ItemActivate += OnItemActivateEvent;
            MediatingForm.Shown += OnFormShownEvent;
        }

        private void OnItemActivateEvent(object sender, EventArgs e)
        {
            //abrir uma janela com a descrição da acção para editar (Reutilizar addwindow)
        }

        private void OnFormShownEvent(object sender, EventArgs e)
        {
            MediatingForm.ActionsList.Items.Clear();
            
            foreach (var userAction in Program.Settings.Actions)
            {
                MediatingForm.ActionsList.Items.Add(userAction.ActionName);
            }
        }

        private void OnAddActionButtonEvent(object sender, EventArgs e)
        {
            Program.GetForm<AddActionWindow>().Show();
        }

        private void OnCancelButtonEvent(object sender, EventArgs e)
        {
            MediatingForm.Hide();
            Program.GetForm<LoggerWindow>().Show();
        }

        private void OnSaveButtonEvent(object sender, EventArgs e)
        {
            Program.Settings.Username = MediatingForm.EmaiTextBox.Text;
            Program.Settings.Password = MediatingForm.PasswordTextBox.Text;

            MediatingForm.Hide();
            Program.GetForm<LoggerWindow>().Show();
        }
    }
}
