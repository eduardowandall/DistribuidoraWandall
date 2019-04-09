using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistribuidoraWandall.Reports
{
    public class ReportPedido
    {
        public string NomeCliente { get; set; }
        public double Total { get; set; }
        public DateTime DataPedido { get; set; }
        public IEnumerable<ReportProduto> Produtos { get; set; }
    }
}
