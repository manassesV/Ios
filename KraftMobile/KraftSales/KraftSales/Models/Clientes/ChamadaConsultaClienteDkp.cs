using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace KraftSales.Models.Clientes
{
    public class ChamadaConsultaClienteDkp
    {
        [JsonProperty("CliCgc")]
        public string Cnpj { get; set; }

        public ChamadaConsultaClienteDkp()
        {

        }

        public ChamadaConsultaClienteDkp(string cnpj)
        {
            if (!string.IsNullOrWhiteSpace(cnpj))
            {
                Cnpj = Regex.Replace(cnpj, @"[^0-9]", "");
            }
        }
    }
}
