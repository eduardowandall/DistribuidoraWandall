using DistribuidoraWandall.Components;
using DistribuidoraWandall.DB;
using System.Windows;
using System.Windows.Controls;

namespace DistribuidoraWandall
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var mapper = LiteDB.BsonMapper.Global;

            mapper.Entity<Pedido>()
                .DbRef(x => x.Cliente, DBEntities.tb_cliente);
            mapper.Entity<PedidoProduto>()
                .DbRef(x => x.Produto, DBEntities.tb_produto);

            InitializeComponent();
            CriarGridProdutos();
        }
        // <Grid.RowDefinitions>
        //    <RowDefinition Height = "45" />
        //</ Grid.RowDefinitions >
        //< Grid.ColumnDefinitions >
        //    < ColumnDefinition />
        //</ Grid.ColumnDefinitions >
        //< local:ProdutoPedidoItem GotFocus = "ProdutoPedidoItem_GotFocus" Grid.Row="0" Grid.Column="0">

        //</local:ProdutoPedidoItem>

        private void CriarGridProdutos()
        {
            var novoItem = new ProdutoPedidoItem();

            var row = new RowDefinition();
            row.Height = new GridLength(novoItem.Height, GridUnitType.Pixel);
            gridProdutos.RowDefinitions.Add(new RowDefinition());
            gridProdutos.ColumnDefinitions.Add(new ColumnDefinition());
            novoItem.GotFocus += NovoItem_GotFocus;
            gridProdutos.Children.Add(novoItem);
            Grid.SetRow(novoItem, gridProdutos.RowDefinitions.Count - 1);

        }

        private void NovoItem_GotFocus(object sender, RoutedEventArgs e)
        {
            ((ProdutoPedidoItem)sender).Testezinho += MainWindow_Testezinho;
        }

        private void MainWindow_Testezinho(object sender, System.EventArgs data)
        {
            if (((ProdutoPedidoItem)sender).IsEmpty)
            {
                gridProdutos.RowDefinitions.Add(new RowDefinition());
                var novoItem = new ProdutoPedidoItem();
                gridProdutos.Children.Add(novoItem);
                Grid.SetRow(novoItem, gridProdutos.RowDefinitions.Count - 1);
            }
        }
    }
}
