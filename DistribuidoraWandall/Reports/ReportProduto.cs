namespace DistribuidoraWandall.Reports
{
    public class ReportProduto
    {
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public double ValorUnitario { get; set; }
        public double ValorTotal => Quantidade * ValorUnitario;
    }
}