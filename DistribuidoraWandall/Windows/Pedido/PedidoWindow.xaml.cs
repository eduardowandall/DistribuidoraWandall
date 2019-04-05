﻿using DistribuidoraWandall.Controllers;
using DistribuidoraWandall.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for Pedido.xaml
    /// </summary>
    public partial class PedidoWindow : UserControl
    {
        sealed class ClienteViewModel : INotifyPropertyChanged
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

            public IReadOnlyList<Cliente> Items
            {
                get { return ClientesController.Instance.Buscar(); }
            }

            Cliente selectedItem;
            public Cliente SelectedItem
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

        private ClienteViewModel ViewModel { get; set; }
        public Cliente SelectedCostumer => ViewModel.SelectedItem;


        public PedidoWindow()
        {
            InitializeComponent();
            ViewModel = new ClienteViewModel();
            Cliente.DataContext = ViewModel;
        }

        private void Salvar_Click(object sender, RoutedEventArgs e)
        {
            var produtos = ListaProdutos.Produtos.Select(x => x.SelectedProduct);
            var opa = new DB.Pedido() {
                Cliente = SelectedCostumer,
                DataPedido = DateTime.Now,
                
            };
        }

        private void Imprimir_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}