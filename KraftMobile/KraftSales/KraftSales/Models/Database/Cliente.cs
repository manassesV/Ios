using Realms;

namespace KraftSales.Models.Database
{
    public class Cliente : RealmObject
    {

        /// <summary>
        /// CNPJ
        /// </summary>
        [PrimaryKey]
        public string CliCgc { get; set; }

        /// <summary>
        /// Nome Fantasia
        /// </summary>
        public string CliNom { get; set; }

        /// <summary>
        /// Razão Social
        /// </summary>
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
        public string CliDtUltAlt { get; set; }
    }
}
