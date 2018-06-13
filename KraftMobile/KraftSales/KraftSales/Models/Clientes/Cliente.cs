using System;

namespace KraftSales.Models.Clientes
{
    public class Cliente
    {
        public Cliente()
        {
            ClienteId = Guid.NewGuid().ToString();
            TipoCliente = Enums.TipoCliente.PessoaJuridica;
        }

        public string ClienteId { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string NomeContato { get; set; }
        public string EmailContato { get; set; }
        public string TelefoneContato { get; set; }
        public DateTime? DataUltimaCompra { get; set; }
        public bool EhNovo { get; set; }
        public bool Ativo { get; set; }
        public Enums.TipoCliente TipoCliente { get; set; }
        public Database.Cliente ClienteInfo { get; set; }
    }

    
}
