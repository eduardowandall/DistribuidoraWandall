using DistribuidoraWandall.Controllers;
using DistribuidoraWandall.DB;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace DistribuidoraWandall.Components
{
    /// <summary>
    /// Interaction logic for ProdutoPedidoItem.xaml
    /// </summary>
    public partial class ProdutoPedidoItem : UserControl
    {
        sealed class ViewModel: INotifyPropertyChanged
        {
            #region INotifyPropertyChanged
            public event PropertyChangedEventHandler PropertyChanged;

            void SetField<X>(ref X field, X value, [CallerMemberName] string propertyName = null)
            {
                if (EqualityComparer<X>.Default.Equals(field, value)) return;

                field = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion

            public IReadOnlyList<Produto> Items
            {
                get { return ProdutosController.Instance.Buscar(); }
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
        public ProdutoPedidoItem()
        {
            InitializeComponent();
            gridProdutos.DataContext = new ViewModel();
        }

        private void Quantidade_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ValorUnitario_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Produto_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            var viewModel = (ViewModel)gridProdutos.DataContext;
            ValorUnitario.Text = viewModel.SelectedItem?.ValorVenda.ToString();
        }
    }
}
