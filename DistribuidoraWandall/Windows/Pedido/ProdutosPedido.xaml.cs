using DistribuidoraWandall.Windows.Pedido.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DistribuidoraWandall.Windows.Pedido
{
    /// <summary>
    /// Interaction logic for ProdutoPedido.xaml
    /// </summary>
    public partial class ProdutosPedido : UserControl
    {
        public ProdutosPedido()
        {
            InitializeComponent();
            PopularGridProdutos();
        }


        public IEnumerable<ProdutoPedidoItem> Produtos => gridProdutos.Children.Cast<ProdutoPedidoItem>();
        private void PopularGridProdutos()
        {
            var novoItem = new ProdutoPedidoItem();

            var row = new RowDefinition();
            row.Height = new GridLength(novoItem.Height, GridUnitType.Pixel);
            gridProdutos.ColumnDefinitions.Add(new ColumnDefinition());
            gridProdutos.RowDefinitions.Add(new RowDefinition());
            novoItem.OnProductSelected += MainWindow_Testezinho;
            gridProdutos.Children.Add(novoItem);
            Grid.SetRow(novoItem, gridProdutos.RowDefinitions.Count - 1);
        }

        private void MainWindow_Testezinho(object sender, EventArgs data)
        {
            if (!(Produtos.LastOrDefault()?.IsEmpty).GetValueOrDefault())
            {
                gridProdutos.RowDefinitions.Add(new RowDefinition());
                var novoItem = new ProdutoPedidoItem();
                novoItem.OnProductSelected += MainWindow_Testezinho;
                gridProdutos.Children.Add(novoItem);
                Grid.SetRow(novoItem, gridProdutos.RowDefinitions.Count - 1);
            }
        }
    }
}
