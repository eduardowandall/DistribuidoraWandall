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

            int quantidade = 1;
            public int Quantidade
            {
                get { return quantidade; }
                set
                {
                    SetField(ref quantidade, value);
                    UpdateTotal();
                }
            }

            double valorUnitario;
            public double ValorUnitario
            {
                get { return valorUnitario; }
                set
                {
                    SetField(ref valorUnitario, value);
                    UpdateTotal();
                }
            }

            public double Total => ValorUnitario * Quantidade;


            private void UpdateTotal()
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Total)));
            }
            public ProdutoPedido ObterProdutoPedido(DB.Produto temp)
            {
                return new ProdutoPedido()
                {
                    Produto = SelectedItem != null ? SelectedItem : temp,
                    Quantidade = Quantidade,
                    ValorVendido = ValorUnitario
                };
            }
        }

        public bool IsEmpty { get { return ViewModel.SelectedItem == null && string.IsNullOrWhiteSpace(Produto.Text); } }

        public event EventHandler OnProductSelected;

        private PedidoProdutoViewModel ViewModel { get; set; }
        public ProdutoPedido SelectedOrderProduct => ViewModel.ObterProdutoPedido(new DB.Produto()
        {
            Nome = Produto.Text,
            ValorVenda = ViewModel.ValorUnitario
        });

        public ProdutoPedidoItem()
        {
            InitializeComponent();
            ViewModel = new PedidoProdutoViewModel();
            ViewModel.PropertyChanged += ProdutoPedidoItem_PropertyChanged;
            gridProdutos.DataContext = ViewModel;
        }

        private void ProdutoPedidoItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedItem" && ViewModel.SelectedItem != null)
            {
                //ValorUnitario.Text = SelectedProduct.ValorVenda.ToString();
                ViewModel.ValorUnitario = ViewModel.SelectedItem.ValorVenda;
            }
        }

        private void AddProduto_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OnProductSelected?.Invoke(this, e);
        }
    }
}
