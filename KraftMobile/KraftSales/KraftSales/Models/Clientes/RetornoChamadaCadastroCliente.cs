namespace KraftSales.Models.Clientes
{
    public class RetornoChamadaCadastroCliente
    {
        public Dados cliente { get; set; }

        public RetornoChamadaCadastroCliente()
        {

        }

        public RetornoChamadaCadastroCliente(string msgErro)
        {
            cliente = new Dados { CliCgc = "", retorno = msgErro };
        }
    }

    public class Dados
    {
        public string CliCgc { get; set; }
        public string retorno { get; set; }
    }

    public class RetornoChamadaCadastroClienteJaExistente
    {
        public string CliCgc { get; set; }
        public string CliNom { get; set; }
        public string CliNomRdz { get; set; }
        public string CliEndFatSep { get; set; }
        public string NroEndFat { get; set; }
        public string ComplEndFat { get; set; }
        public string CliBaiFat { get; set; }
        public string CliCidFat { get; set; }
        public string CliEstFat { get; set; }
        public string CliCepFat { get; set; }
        public string CliDddCom { get; set; }
        public string CliTelCom { get; set; }
        public string CliInsFat { get; set; }
        public string CliCntCom { get; set; }
        public string CliEndEle { get; set; }
        public string CliInsSfr { get; set; }
        public string CliMunSfr { get; set; }
    }
}






