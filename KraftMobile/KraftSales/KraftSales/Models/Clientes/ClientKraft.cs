using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace KraftSales.Models.Clientes
{
    public class ClienteKraft
    {
        public ClienteKraft()
        {

        }

        public ClienteKraft(Cliente cliente)
        {
            if (cliente != null)
            {
                Cnpj = cliente.Cnpj;
                NomeReduzido = cliente.NomeFantasia;
                Nome = cliente.RazaoSocial;
                Contato = cliente.NomeContato;
                Email = cliente.EmailContato;

                //string telefoneLimpo = cliente.TelefoneContato.Replace("(", "").Replace(")", "").Replace("-", "");
                string telefoneLimpo = Regex.Replace(cliente.TelefoneContato ?? string.Empty, @"[^0-9]", "");
                if (telefoneLimpo.Length > 1)
                {
                    Ddd = telefoneLimpo.Substring(0, 2);
                }

                if (telefoneLimpo.Length > 2)
                {
                    Telefone = telefoneLimpo.Substring(2);
                }
            }
        }

        [JsonProperty("CliCgc")]
        public string Cnpj { get; set; } = string.Empty;
        [JsonProperty("CliNom")]
        public string Nome { get; set; } = string.Empty;
        [JsonProperty("CliNomRdz")]
        public string NomeReduzido { get; set; } = string.Empty;
        [JsonProperty("CliEndFatSep")]
        public string EnderecoLogradouro { get; set; } = string.Empty;
        [JsonProperty("NroEndFat")]
        public string EnderecoNumero { get; set; } = string.Empty;
        [JsonProperty("ComplEndFat")]
        public string EnderecoComplemento { get; set; } = string.Empty;
        [JsonProperty("CliBaiFat")]
        public string EnderecoBairro { get; set; } = string.Empty;
        [JsonProperty("CliCidFat")]
        public string Cidade { get; set; } = string.Empty;
        [JsonProperty("CliEstFat")]
        public string Estado { get; set; } = string.Empty;
        [JsonProperty("CliCepFat")]
        public string Cep { get; set; } = string.Empty;
        [JsonProperty("CliDddCom")]
        public string Ddd { get; set; } = string.Empty;
        [JsonProperty("CliTelCom")]
        public string Telefone { get; set; } = string.Empty;
        [JsonProperty("CliInsFat")]
        public string InscricaoEstadual { get; set; } = string.Empty;
        [JsonProperty("CliCntCom")]
        public string Contato { get; set; } = string.Empty;
        [JsonProperty("CliEndEle")]
        public string Email { get; set; } = string.Empty;
        [JsonProperty("CliInsSfr")]
        public string CliInsSfr { get; set; } = string.Empty;
        [JsonProperty("CliMunSfr")]
        public string CliMunSfr { get; set; } = string.Empty;
    }
}
