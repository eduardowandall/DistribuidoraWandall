using System;
using System.Collections.Generic;

namespace DistribuidoraWandall.DB
{
    public class Pedido
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime DataPedido { get; set; }
        public List<ProdutoPedido> Produtos { get; set; }
        public MetodoPagamentoEnum MetodoPagamento { get; set; }

    }

    public class ProdutoPedido
    {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public double ValorVendido { get; set; }
        public double ValorTotal => ValorVendido * Quantidade;
    }

    public enum MetodoPagamentoEnum
    {
        Dinheiro, CartaoDebito, CartaoCredito, ValeAlimentacao, Outro
    }
}