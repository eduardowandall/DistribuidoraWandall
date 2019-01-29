using DistribuidoraWandall.Components;
using DistribuidoraWandall.DB;
using System.Collections.Generic;
using System.Linq;
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
        }


    }
}
