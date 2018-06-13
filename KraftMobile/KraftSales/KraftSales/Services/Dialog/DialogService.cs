using Acr.UserDialogs;
using System;
using System.Threading.Tasks;

namespace KraftSales.Services
{
    public class DialogService : IDialogService
    {
        public Task ShowAlertAsync(string message, string title, string buttonLabel)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }

        public Task<bool> ConfirmAsync(string message, string title, string okText, string cancelText)
        {
            return UserDialogs.Instance.ConfirmAsync(message, title, okText, cancelText);
        }

        public IDisposable Toast(string title, int seconds)
        {
            return UserDialogs.Instance.Toast(title, new TimeSpan(0, 0, seconds));
        }

        public void ShowSuccess(string message, int seconds)
        {
            UserDialogs.Instance.ShowSuccess(message, seconds * 1000);
        }

        public void ShowError(string message, int seconds)
        {
            UserDialogs.Instance.ShowError(message, seconds * 1000);
        }
    }
}
