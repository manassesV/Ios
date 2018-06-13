using System;
using System.Threading.Tasks;

namespace KraftSales.Services
{
    public interface IDialogService
    {
        Task ShowAlertAsync(string message, string title, string buttonLabel);
        Task<bool> ConfirmAsync(string message, string title, string okText, string cancelText);
        IDisposable Toast(string title, int seconds);
        void ShowSuccess(string message, int seconds);
        void ShowError(string message, int seconds);
    }
}
