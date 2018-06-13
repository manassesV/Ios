using KraftSales.Helpers;
using KraftSales.Models.Usuarios;
using KraftSales.Services.Base;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KraftSales.Services.Usuarios
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        public UsuarioService() : base("")
        {

        }

        public async Task<Usuario> AuthUser(string login, string password)
        {
            try
            {
                var userToAuth = new UsuarioChamadaAutenticacaoPortal(login, password);
                var json = JsonConvert.SerializeObject(userToAuth);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var message = await client.PostAsync($"{Settings.UrlPortalBase}/ge1G82s9lv/android/user/auth", content);
                var result = string.Empty;

                if (message.IsSuccessStatusCode)
                {
                    using (var responseStream = await message.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        result = await new StreamReader(responseStream).ReadToEndAsync().ConfigureAwait(false);
                    }

                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        var retorno1 = JsonConvert.DeserializeObject<UsuarioRetornoPortal>(result);

                        if (retorno1 != null && retorno1.user != null && retorno1.user.Any())
                        {
                            var retUser = retorno1.user.First();

                            var userAuthenticated = new Usuario
                            {
                                Ativo = true,
                                CodigoRepresentante = retUser.userCodigoMK.ToString(),
                                Login = retUser.userEmail,
                                Nome = retUser.userName,
                                Senha = string.Empty,
                                TipoUsuario = retUser.userProfileName,
                                UsuarioId = retUser.userCodigoMK.ToString(),
                                GridItems = retorno1.grid,
                                MensagemRetornoAutenticacao = string.Empty,
                            };
                            return await Task.FromResult(userAuthenticated);
                        }
                        else
                        {
                            var retorno2 = JsonConvert.DeserializeObject<UsuarioRetornoComErro>(result);
                            if (retorno2 != null && !string.IsNullOrWhiteSpace(retorno2.retorno))
                            {
                                return await Task.FromResult(new Usuario(retorno2.retorno));
                            }
                            else
                            {
                                return await Task.FromResult(new Usuario("Erro ao autenticar usuário"));
                            }
                        }
                        
                    }
                    else
                    {
                        return await Task.FromResult(new Usuario("Erro ao autenticar usuário"));
                    }
                }
                else
                {
                    return await Task.FromResult(new Usuario("Erro ao autenticar usuário"));
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
                return await Task.FromResult(new Usuario("Erro ao autenticar usuário"));
            }
        }
    }
}
