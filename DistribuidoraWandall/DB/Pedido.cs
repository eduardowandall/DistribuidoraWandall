using System;
using System.Collections.Generic;

namespace DistribuidoraWandall.DB
{
    public class Pedido
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime DataPedido { get; set; }
        public List<PedidoProduto> Produtos { get; set; }
    }

    public class PedidoProduto
    {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public double ValorVendido { get; set; }
    }
}