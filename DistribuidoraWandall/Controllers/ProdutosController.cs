using DistribuidoraWandall.DB;
using System.Collections.Generic;
using System.Linq;

namespace DistribuidoraWandall.Controllers
{
    public class ProdutosController
    {
        private static ProdutosController _instance;
        public static ProdutosController Instance => _instance ?? new ProdutosController();

        private static List<Produto> _produtos;

        private ProdutosController()
        {

        }

        public List<Produto> Buscar()
        {
            if (_produtos == null)
                using (var db = new DB.DBEntities())
                {
                    var produtos = db.Produtos
                        .FindAll().OrderByDescending(x => x.Id).ToList();
                    produtos.ForEach(x => x.Nome = x.Nome + $" - ({x.Id})");
                    _produtos = produtos;
                }
            return _produtos;
        }
        public object Apagar(int id)
        {
            return SetProdutoApagado(id, true);
        }
        public object DesfazerApagar(int id)
        {
            return SetProdutoApagado(id, false);
        }

        private object SetProdutoApagado(int id, bool apagado)
        {
            using (var db = new DB.DBEntities())
            {
                var produto = db.Produtos.FindOne(x => x.Id == id);
                produto.Apagado = apagado;
                db.Produtos.Update(produto);
                return null;
            }
        }

        public object Salvar(DB.Produto pr)
        {
            using (var db = new DB.DBEntities())
            {
                if (pr.Id > 0)
                    db.Produtos.Update(pr);
                else
                    db.Produtos.Insert(pr);

                return null;
            }
        }
    }
}
