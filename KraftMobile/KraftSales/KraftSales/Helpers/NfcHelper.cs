using KraftSales.ViewModels.Base;
using Xamarin.Forms;

namespace KraftSales.Helpers
{
    public class NfcHelper
    {
        public void AdicionarMensagemLida(string valorTag)
        {
            MessagingCenter.Send(this, MessageKeys.NfcTagRead, valorTag);
        }
    }
}
