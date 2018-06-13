using System.Collections.Generic;
using System.Threading.Tasks;

namespace KraftSales.Services.EnvioEmails
{
    public interface IEnvioEmailService
    {
        Task SendMail(string to, string from, string subject, string text, List<string> copies);
    }
}
