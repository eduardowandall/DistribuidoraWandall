using DistribuidoraWandall.Controllers;
using DistribuidoraWandall.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace DistribuidoraWandall.Windows.Pedido
{
    /// <summary>
    /// Interaction logic for Pedido.xaml
    /// </summary>
    public partial class PedidoWindow : UserControl
    {
        public static List<ProdutoPedido> ProdutosDoPedido { get; set; }

        public PedidoWindow()
        {
            InitializeComponent();

            ProdutosDoPedido = new List<ProdutoPedido>{
                new ProdutoPedido()
                {
                    Produto = ProdutosController.Instance.Buscar().FirstOrDefault(),
                    Quantidade = 3,
                    ValorVendido = 43
                },
                new ProdutoPedido()
                {
                    Produto = ProdutosController.Instance.Buscar().FirstOrDefault(),
                    Quantidade = 3,
                    ValorVendido = 43
                },
                new ProdutoPedido()
                {
                    Produto = ProdutosController.Instance.Buscar().FirstOrDefault(),
                    Quantidade = 3,
                    ValorVendido = 43

                },
            };

            AddProduto.OnProductSelected += AddProduto_OnProductSelected;

            this.gridProdutos.ItemsSource = ProdutosDoPedido;
        }

        private void AddProduto_OnProductSelected(object sender, EventArgs e)
        {
            var produtoExistente = ProdutosDoPedido.FirstOrDefault(x => x.Produto.Id == AddProduto.SelectedOrderProduct.Produto.Id);
            if (produtoExistente != null)
                produtoExistente.Quantidade += 1;
            else
                ProdutosDoPedido.Add(AddProduto.SelectedOrderProduct);

            gridProdutos.Items.Refresh();
        }

        private void Salvar_Click(object sender, RoutedEventArgs e)
        {
            SalvarPedido();
        }

        private void Imprimir_Click(object sender, RoutedEventArgs e)
        {
            var pedido = SalvarPedido();
        }

        private DB.Pedido SalvarPedido()
        {
            var pedido = new DB.Pedido()
            {
                DataPedido = DateTime.Now,
                Produtos = ProdutosDoPedido
            };
            PedidosController.Instance.Salvar(pedido);
            return pedido;
        }
        string barCode = "";
        private void UserControl_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                e.Handled = true;

            if (Utils.KeysUtils.IsKeyNumber(e.Key))
                barCode += Utils.KeysUtils.KeyToNumber(e.Key);

            if (barCode.Length == 13)
            {
                var foundItem = ProdutosController.Instance.Buscar(barCode);
                barCode = string.Empty;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var idProduto = (int)((Button)e.OriginalSource).CommandParameter;

            var produtoExistente = ProdutosDoPedido.FirstOrDefault(x => x.Produto.Id == idProduto);

            ProdutosDoPedido.Remove(produtoExistente);

            gridProdutos.Items.Refresh();
        }

        private void GridProdutos_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //gridProdutos.Items.Refresh();
        }
        private static bool cellHasEdited;
        private void GridProdutos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cellHasEdited)
            {
                //gridProdutos.Items.Refresh();

                cellHasEdited = false;
            }

        }

        private void GridProdutos_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            
            cellHasEdited = true;
        }
    }
}
