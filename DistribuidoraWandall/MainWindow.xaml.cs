using DistribuidoraWandall.Controllers;
using DistribuidoraWandall.DB;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace DistribuidoraWandall
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        sealed class ViewModel
           : INotifyPropertyChanged
        {
            #region INotifyPropertyChanged
            public event PropertyChangedEventHandler PropertyChanged;

            void SetField<X>(ref X field, X value, [CallerMemberName] string propertyName = null)
            {
                if (EqualityComparer<X>.Default.Equals(field, value)) return;

                field = value;

                var h = PropertyChanged;
                if (h != null) h(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion

            public IReadOnlyList<Produto> Items
            {
                get { return new ProdutosController().Buscar(); }
            }

            Produto selectedItem;
            public Produto SelectedItem
            {
                get { return selectedItem; }
                set { SetField(ref selectedItem, value); }
            }

            long? selectedValue;
            public long? SelectedValue
            {
                get { return selectedValue; }
                set { SetField(ref selectedValue, value); }
            }
        }
        public MainWindow()
        {
            var mapper = LiteDB.BsonMapper.Global;

            mapper.Entity<Pedido>()
                .DbRef(x => x.Cliente, DBEntities.tb_cliente);
            mapper.Entity<PedidoProduto>()
                .DbRef(x => x.Produto, DBEntities.tb_produto);
            var prod = new ProdutosController().Buscar();

            InitializeComponent();
            gridProdutos.DataContext = new ViewModel();
        }
    }
}
