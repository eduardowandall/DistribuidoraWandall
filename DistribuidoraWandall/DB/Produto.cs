namespace DistribuidoraWandall.DB
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double ValorCompra { get; set; }
        public double ValorVenda { get; set; }
        public bool Apagado { get; set; }
        public bool Temporario { get; set; }
    }
}