using System.Collections.Generic;

namespace KraftSales.Models.Usuarios
{
    public class Usuario
    {
        public string UsuarioId { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string TipoUsuario { get; set; }
        public bool Ativo { get; set; }
        public string CodigoRepresentante { get; set; }
        public string MensagemRetornoAutenticacao { get; set; }
        public ICollection<GridReturn> GridItems { get; set; }

        public Usuario()
        {
            GridItems = new List<GridReturn>();
        }

        public Usuario(string mensagemErro)
        {
            MensagemRetornoAutenticacao = mensagemErro;
            GridItems = new List<GridReturn>();
        }
    }
    
    public class UsuarioChamadaAutenticacaoPortal
    {
        public UsuarioChamadaAutenticacaoPortal(string email, string password)
        {
            user = new UserCall { email = email, password = password };
        }

        public UserCall user { get; set; }
    }

    public class UserCall
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    public class UsuarioRetornoComErro
    {
        public string retorno { get; set; }
    }

    public class UsuarioRetornoPortal
    {
        public UsuarioRetornoPortal()
        {
            user = new List<UserReturn>();
            grid = new List<GridReturn>();
        }

        public List<UserReturn> user { get; set; }
        public List<GridReturn> grid { get; set; }
    }

    public class UserReturn
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }
        public int userCodigoMK { get; set; }
        public int userProfileId { get; set; }
        public string userProfileName { get; set; }
        public int userGroupId { get; set; }
        public string userGroupName { get; set; }
        public string userPass { get; set; }
    }

    public class GridReturn
    {
        public string productReference { get; set; }
        public string productDescription { get; set; }
        public string productImage { get; set; }
        public int totalPackQty { get; set; }
        public ItemGridReturn[] items { get; set; }
    }

    public class ItemGridReturn
    {
        public int packQty { get; set; }
        public string productSize { get; set; }
        public string productColor { get; set; }
    }

}

