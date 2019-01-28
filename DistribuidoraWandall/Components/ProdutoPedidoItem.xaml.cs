using DistribuidoraWandall.Controllers;
using DistribuidoraWandall.DB;
using System;
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
        //TODO - Criar controllers para as classes(precisa criar prop pra mostrar e pesquisar Id e Nome. e prop para total)
        sealed class PedidoProdutoViewModel: INotifyPropertyChanged
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
        }

        public bool IsEmpty { get { return selectedProduct == null; } }

        public delegate void EventHandler(object sender, EventArgs data);
        public event EventHandler Testezinho;

        private PedidoProdutoViewModel ViewModel => (PedidoProdutoViewModel)gridProdutos.DataContext;
        private Produto selectedProduct => ViewModel.SelectedItem;

        public ProdutoPedidoItem()
        {
            InitializeComponent();
            gridProdutos.DataContext = new PedidoProdutoViewModel();
        }

        private void Quantidade_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Testezinho != null)
                Testezinho(this, e);
        }

        private void ValorUnitario_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Testezinho != null)
                Testezinho(this, e);
        }

        private void Produto_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            ValorUnitario.Text = selectedProduct?.ValorVenda.ToString();
            if (Testezinho != null)
                Testezinho(this, e);
        }
    }
}
