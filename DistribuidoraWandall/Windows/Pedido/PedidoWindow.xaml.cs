using DistribuidoraWandall.Controllers;
using DistribuidoraWandall.DB;
using DistribuidoraWandall.Reports;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
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
            var produtos = ListaProdutos.Produtos.Select(x => x.SelectedOrderProduct);
            var opa = new DB.Pedido()
            {
                Cliente = SelectedCostumer,
                DataPedido = DateTime.Now,
                Produtos = produtos.ToList()
            };
            PedidosController.Instance.Salvar(opa);
        }

        private void Imprimir_Click(object sender, RoutedEventArgs e)
        {
            var report = new LocalReport();
            report.ReportEmbeddedResource = "DistribuidoraWandall.Reports.Pedidos.rdlc";
            var opa = PedidosController.Instance.BuscarPorId(42);
            report.DataSources.Add(new ReportDataSource("Produtos", opa.Produtos.Select(x => new ReportProduto()
            {
                Nome = x.Produto.Nome,
                Quantidade = x.Quantidade,
                ValorUnitario = x.ValorVendido
            })));
            report.PrintToPrinter();
        }
        private string ReadEmbeddedResource(string ResourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(ResourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                string temp = result.Replace('\r', ' ');
                return temp;
            }
        }
    }
}
