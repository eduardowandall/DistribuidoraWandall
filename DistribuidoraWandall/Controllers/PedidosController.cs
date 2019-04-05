using DistribuidoraWandall.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DistribuidoraWandall.Controllers
{
    public class PedidosController : BaseController
    {

        private static PedidosController _instance;
        public static PedidosController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PedidosController();
                return _instance;
            }
        }

        private PedidosController()
        {

        }

        public object Imprimir(int id)
        {
            using (var db = new DB.DBEntities())
            {
                var pedidoDB = db.Pedidos
                    .Include(x => x.Cliente)
                    .Include(x => x.Produtos[0].Produto)
                    .FindOne(x => x.Id == id);
                return new
                {
                    DataPedido = pedidoDB.DataPedido.ToString("dd/MM/yyyy"),
                    NomeCliente = pedidoDB.Cliente.Nome,
                    EnderecoCliente = pedidoDB.Cliente.Endereco?.ToString(),
                    Produtos = pedidoDB.Produtos.Select(x => new { x.Produto.Nome, x.Quantidade, x.ValorVendido, ValorTotal = x.Quantidade * x.ValorVendido }),
                    ValorTotal = pedidoDB.Produtos.Sum(x => x.ValorVendido * x.Quantidade)
                };
            }
        }
        public object Apagar(int id)
        {
            using (var db = new DB.DBEntities())
            {
                var pedidoDB = db.Pedidos
                    .Include(x => x.Cliente)
                    .Include(x => x.Produtos[0].Produto)
                    .FindOne(x => x.Id == id);
                var rowsModified = db.Pedidos.Delete(x => x.Id == id);
                if (rowsModified >= 1)
                    return null; //foi
                else
                    return null; //não foi
            }
        }
        public List<Pedido> Buscar()
        {
            using (var db = new DB.DBEntities())
            {
                var pedidos = db.Pedidos
    .Include(x => x.Cliente)
    .FindAll().ToList();
                return pedidos;

                //var pedidos = db.Pedidos
                //    .Include(x => x.Cliente)
                //    .FindAll().Select(x => new
                //    {
                //        x.Id,
                //        x.DataPedido,
                //        x.Cliente,
                //        ValorTotal = x.Produtos == null ? 0 : x.Produtos.Sum(y => y.ValorVendido * y.Quantidade),
                //    }).ToList();
                //return pedidos;
            }
        }
        public object BuscarPorId(int id)
        {
            using (var db = new DB.DBEntities())
            {
                var pedido = db.Pedidos
                    .Include(x => x.Cliente)
                    .Include(x => x.Produtos[0].Produto)
                    .FindOne(x => x.Id == id);
                return pedido;
            }
        }
        public object Salvar(DB.Pedido pd)
        {
            using (var db = new DB.DBEntities())
            {
                pd.DataPedido = DateTime.Now;

                if (pd.Id > 0)
                    db.Pedidos.Update(pd);
                else
                    db.Pedidos.Insert(pd);

                return pd.Id;
            }
        }
    }
}
