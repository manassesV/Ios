using KraftSales.Extensions;
using KraftSales.Helpers;
using KraftSales.Models.Produtos;
using KraftSales.Models.Usuarios;
using KraftSales.Services.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KraftSales.Services.Produtos
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoSoapService produtoSoapService;

        public ProdutoService() : base("")
        {
            produtoSoapService = DependencyService.Get<IProdutoSoapService>();
        }

        private ObservableCollection<Produto> _mockProdutos = new ObservableCollection<Produto>
        {
            new Produto
            {
                Codigo = "P01", Descricao = "KHELF - 13944", GrupoId = "14", DetalheId = "5", Preco = 49.99,
                ItensPackMinimo = new List<Models.Database.GradeProduto>(),
                //{
                //    new ItemPack{ QuantidadeTamanhoG = 2, QuantidadeTamanhoM = 4, QuantidadeTamanhoP = 2, Cor = "Preta" },
                //    new ItemPack{ QuantidadeTamanhoG = 3, QuantidadeTamanhoM = 6, QuantidadeTamanhoP = 3, Cor = "Branco" },
                //},
                ItensCaixaMinimo = new List<ItemPack>(),
                //{
                //    new ItemPack{ QuantidadeTamanhoG = 10, QuantidadeTamanhoM = 20, QuantidadeTamanhoP = 10, Cor = "Preta" },
                //    new ItemPack{ QuantidadeTamanhoG = 15, QuantidadeTamanhoM = 30, QuantidadeTamanhoP = 15, Cor = "Branco" },
                //}, 
                QuantidadeCaixasEmEstoque = 10, QuantidadePacksEmEstoque = 10
            },

            //new Produto
            //{
            //    Codigo = "P02", Descricao = "BLUSA FEM INV", GrupoId = "1", DetalheId = "0", Preco = 29.99,
            //    ItensPackMinimo = new List<ItemPack>
            //    {
            //        new ItemPack{ QuantidadeTamanhoG = 2, QuantidadeTamanhoM = 4, QuantidadeTamanhoP = 2, Cor = "Preta" },
            //        new ItemPack{ QuantidadeTamanhoG = 3, QuantidadeTamanhoM = 6, QuantidadeTamanhoP = 3, Cor = "Branco" },
            //    },
            //    ItensCaixaMinimo = new List<ItemPack>
            //    {
            //        new ItemPack{ QuantidadeTamanhoG = 10, QuantidadeTamanhoM = 20, QuantidadeTamanhoP = 10, Cor = "Preta" },
            //        new ItemPack{ QuantidadeTamanhoG = 15, QuantidadeTamanhoM = 30, QuantidadeTamanhoP = 15, Cor = "Branco" },
            //    }, QuantidadeCaixasEmEstoque = 10, QuantidadePacksEmEstoque = 10
            //},

            //new Produto
            //{
            //    Codigo = "P03", Descricao = "BLUS FEM - CMF77518", GrupoId = "14", DetalheId = "5", Preco = 39.99,
            //    ItensPackMinimo = new List<ItemPack>
            //    {
            //        new ItemPack{ QuantidadeTamanhoG = 2, QuantidadeTamanhoM = 4, QuantidadeTamanhoP = 2, Cor = "Preta" },
            //        new ItemPack{ QuantidadeTamanhoG = 3, QuantidadeTamanhoM = 6, QuantidadeTamanhoP = 3, Cor = "Branco" },
            //    },
            //    ItensCaixaMinimo = new List<ItemPack>
            //    {
            //        new ItemPack{ QuantidadeTamanhoG = 10, QuantidadeTamanhoM = 20, QuantidadeTamanhoP = 10, Cor = "Preta" },
            //        new ItemPack{ QuantidadeTamanhoG = 15, QuantidadeTamanhoM = 30, QuantidadeTamanhoP = 15, Cor = "Branco" },
            //    }, QuantidadeCaixasEmEstoque = 10, QuantidadePacksEmEstoque = 10
            //},

        };

        public async Task<ObservableCollection<Produto>> GetAllProductsMockAsync()
        {
            await Task.Delay(500);
            return _mockProdutos;
        }

        public async Task<Produto> GetProdutoByCodigoMockAsync(string codigo)
        {
            var produto = await GetProdutoAsync(codigo);
            if (produto != null)
            {
                var mockProduto = _mockProdutos.First();
                mockProduto.Codigo = codigo;
                mockProduto.Descricao = produto.Descricao;
                mockProduto.GrupoId = produto.Grupo;
                mockProduto.DetalheId = produto.Detalhe;
                mockProduto.Preco = produto.Preco;

                mockProduto.ProdutoInfo = produto;

                return mockProduto;
            }
            return null;
        }

        public async Task<Produto> GetProdutoByIdMockAsync(string id)
        {
            await Task.Delay(200);
            return _mockProdutos.SingleOrDefault(c => c.ProdutoId == id);
        }




        public async Task<ObservableCollection<Models.Database.Produto>> GetAllOfflineAsync()
        {
            var realm = App.RealmContext.GetContext();
            var items = realm.All<Models.Database.Produto>().ToList();

            if (items != null)
            {
                return await Task.FromResult(items.ToObservableCollection());
            }

            return new ObservableCollection<Models.Database.Produto>();
        }

        public async Task<ObservableCollection<string>> GetAllCodesOfflineAsync()
        {
            var realm = App.RealmContext.GetContext();
            var items = realm.All<Models.Database.Produto>().ToList();

            if (items != null)
            {
                return await Task.FromResult(items.Select(c => c.Codigo).OrderBy(c => c).ToObservableCollection());
            }

            return new ObservableCollection<string>();
        }

        public async Task<ObservableCollection<Models.Database.Produto>> GetAllOnlineAsync()
        {
            try
            {
                string dataUltimaAlteracao = Settings.DataUltimoDownloadProduto;

                //var list = await GetFromWebApi<List<Models.Database.Produto>>($"ListarProdutos?DataUltAlteracao={dataUltimaAlteracao}");
                var list = await produtoSoapService.GetAllOnlineAsync(dataUltimaAlteracao);
                if (list != null)
                {
                    return list.ToObservableCollection();
                }

                Debug.WriteLine("erro ao buscar produtos online");
                return new ObservableCollection<Models.Database.Produto>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new ObservableCollection<Models.Database.Produto>();
            }
        }

        public async Task<Models.Database.Produto> GetProdutoAsync(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
            {
                return null;
            }

            codigo = codigo.ToUpper();

            var realm = App.RealmContext.GetContext();
            var count = realm.All<Models.Database.Produto>().Count();
            var item = Task.FromResult(realm.Find<Models.Database.Produto>(codigo));

            return await (item);
        }

        public async Task<int> GetProdutosOfflineCount()
        {
            var realm = App.RealmContext.GetContext();
            return await Task.FromResult(realm.All<Models.Database.Produto>().Count());
        }

        public async Task<bool> UpsertProdutosAsync(List<Models.Database.Produto> produtos)
        {
            try
            {
                if (produtos != null && produtos.Any())
                {
                    var context = App.RealmContext.GetContext();
                    using (var trans = context.BeginWrite())
                    {
                        foreach (var item in produtos.ToList())
                        {
                            item.Preco = item.Preco / 100;
                            context.Add(item, true);
                        }

                        trans.Commit();
                        Settings.DataUltimoDownloadProduto = DateTime.Now.ToString("dd/MM/yyyy");

                        return await Task.FromResult(true);
                    };
                }

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        private static void RemoveOldItems()
        {
            var realm = App.RealmContext.GetContext();
            var produtosSalvos = realm.All<Models.Database.Produto>();
            foreach (var prod in produtosSalvos.ToList())
            {
                var anoString = prod.DataUltAlteracao.Substring(6);
                int.TryParse(anoString, out int ano);

                if (ano < 2017)
                {
                    realm.Write(() =>
                    {
                        realm.Remove(prod);
                    });
                }
            }
        }

        public async Task<int> GetBalanceAvailable(string codigoProduto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(codigoProduto.Trim()))
                {
                    return await Task.FromResult(0);
                }

                codigoProduto = codigoProduto.ToUpper();

                //var balance = await GetFromWebApi<RetornoSaldo>($"ConsultarSaldo?Referencia={codigoProduto}");
                //if (balance != null)
                //{
                //    int.TryParse(balance.Saldo.disponivel, out int retorno);
                //    return retorno;
                //}
                var balance = await produtoSoapService.GetBalanceAvailable(codigoProduto);
                return balance;

                //Debug.WriteLine("erro ao buscar produtos online");
                //return await Task.FromResult(0);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return await Task.FromResult(0);
            }
        }

        public async Task<bool> UpsertImagemProdutosAsync(List<GridReturn> imagens)
        {
            try
            {
                if (imagens != null && imagens.Any())
                {
                    var context = App.RealmContext.GetContext();
                    using (var trans = context.BeginWrite())
                    {
                        foreach (var item in imagens.ToList())
                        {
                            var image = new Models.Database.ImagemProduto
                            {
                                CodigoProduto = item.productReference.ToUpper(),
                                Url = item.productImage
                            };
                            context.Add(image, true);
                        }

                        trans.Commit();

                        return await Task.FromResult(true);
                    };
                }

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public async Task<string> GetImagemProdutoAsync(string codigoProduto)
        {
            if (string.IsNullOrWhiteSpace(codigoProduto))
            {
                return await Task.FromResult("mochine250x270black");
            }

            codigoProduto = codigoProduto.ToUpper();

            var realm = App.RealmContext.GetContext();
            var imagemProduto = realm.Find<Models.Database.ImagemProduto>(codigoProduto);

            if (imagemProduto == null)
            {
                return await Task.FromResult("mochine250x270black");
            }
            else
            {
                return await Task.FromResult(imagemProduto.Url);
            }
        }

        public async Task<bool> UpsertGradeProdutosAsync(List<GridReturn> grids)
        {
            try
            {
                if (grids != null && grids.Any())
                {
                    var context = App.RealmContext.GetContext();

                    //using (var trans = context.BeginWrite())
                    //{
                    //    context.RemoveAll<Models.Database.GradeProduto>();
                    //    trans.Commit();
                    //}

                    using (var trans = context.BeginWrite())
                    {
                        context.RemoveAll<Models.Database.GradeProduto>();

                        foreach (var item in grids.ToList())
                        {
                            if (item.items.Any())
                            {
                                foreach (var gridItem in item.items.ToList())
                                {
                                    var grade = new Models.Database.GradeProduto
                                    {
                                        CodigoProduto = item.productReference.ToUpper(),
                                        Cor = gridItem.productColor,
                                        Quantidade = gridItem.packQty,
                                        Tamanho = gridItem.productSize,
                                    };

                                    context.Add(grade, false);
                                } 
                            }
                        }

                        trans.Commit();

                        return await Task.FromResult(true);
                    };
                }

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public async Task<ObservableCollection<Models.Database.GradeProduto>> GetGradeProduto(string codigoProduto)
        {
            var realm = App.RealmContext.GetContext();
            var items = realm.All<Models.Database.GradeProduto>().ToList().Where(c => c.CodigoProduto.ToUpperInvariant() == codigoProduto.ToUpperInvariant());

            if (items != null)
            {
                return await Task.FromResult(items.ToObservableCollection());
            }

            return new ObservableCollection<Models.Database.GradeProduto>();
        }
    }
}
