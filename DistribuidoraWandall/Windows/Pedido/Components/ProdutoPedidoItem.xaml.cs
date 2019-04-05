using DistribuidoraWandall.Controllers;
using DistribuidoraWandall.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace DistribuidoraWandall.Windows.Pedido.Components
{
    /// <summary>
    /// Interaction logic for ProdutoPedidoItem.xaml
    /// </summary>
    public partial class ProdutoPedidoItem : UserControl
    {
        //TODO - Criar controllers para as classes(precisa criar prop pra mostrar e pesquisar Id e Nome. e prop para total)
        sealed class PedidoProdutoViewModel : INotifyPropertyChanged
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

            double quantidade = 1;
            public double Quantidade
            {
                get { return quantidade; }
                set { SetField(ref quantidade, value); }
            }

            double valorUnitario;
            public double ValorUnitario
            {
                get { return valorUnitario; }
                set { SetField(ref valorUnitario, value); }
            }

            public double Total => ValorUnitario * Quantidade;
        }

        public bool IsEmpty { get { return SelectedProduct == null && string.IsNullOrWhiteSpace(Produto.Text); } }

        public event EventHandler Testezinho;

        private PedidoProdutoViewModel ViewModel { get; set; }
        public Produto SelectedProduct => ViewModel.SelectedItem;

        public ProdutoPedidoItem()
        {
            InitializeComponent();
            ViewModel = new PedidoProdutoViewModel();
            ViewModel.PropertyChanged += ProdutoPedidoItem_PropertyChanged;
            gridProdutos.DataContext = ViewModel;
        }

        private void ProdutoPedidoItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedItem" && SelectedProduct != null)
            {
                //ValorUnitario.Text = SelectedProduct.ValorVenda.ToString();
                ViewModel.ValorUnitario = SelectedProduct.ValorVenda;
                Testezinho?.Invoke(this, e);
            }
        }
    }
}
