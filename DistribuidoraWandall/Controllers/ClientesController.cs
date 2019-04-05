using System.Collections.Generic;
using System.Linq;
using DistribuidoraWandall.DB;

namespace DistribuidoraWandall.Controllers
{
    public class ClientesController : BaseController
    {

        private static ClientesController _instance;
        public static ClientesController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ClientesController();
                return _instance;
            }
        }

        private ClientesController()
        {

        }

        public List<Cliente> Buscar()
        {
            using (var db = new DBEntities())
            {
                var clientes = db.Clientes
                    .Find(x => !x.Apagado).OrderByDescending(x => x.Id);
                return clientes.ToList();
            }
        }
        public object Apagar(int id)
        {
            return SetClienteApagado(id, true);
        }
        public object DesfazerApagar(int id)
        {
            return SetClienteApagado(id, false);
        }
        private object SetClienteApagado(int id, bool apagado)
        {
            using (var db = new DB.DBEntities())
            {
                var cliente = db.Clientes.FindOne(x => x.Id == id);
                cliente.Apagado = apagado;
                db.Clientes.Update(cliente);
                return null;
            }
        }
        public object Salvar(DB.Cliente cl)
        {
            using (var db = new DB.DBEntities())
            {
                if (cl.Id > 0)
                    db.Clientes.Update(cl);
                else
                    db.Clientes.Insert(cl);

                return null;
            }
        }
    }
}
