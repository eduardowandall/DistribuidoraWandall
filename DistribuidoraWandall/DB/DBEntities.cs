using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistribuidoraWandall.DB
{
    public class DBEntities : LiteDatabase
    {
        public const string db_name = "ArquivoDeInformacoes.db";
        public const string tb_cliente = "Clientes";
        public const string tb_produto = "Produtos";
        public const string tb_pedido = "Pedidos";

        public DBEntities() : base(db_name)
        {
        }
        public LiteCollection<Cliente> Clientes { get { return GetCollection<Cliente>(tb_cliente); } }
        public LiteCollection<Produto> Produtos { get { return GetCollection<Produto>(tb_produto); } }
        public LiteCollection<Pedido> Pedidos { get { return GetCollection<Pedido>(tb_pedido); } }
    }
}
