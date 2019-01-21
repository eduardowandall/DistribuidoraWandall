using System.Collections.Generic;

namespace DistribuidoraWandall.DB
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string InscricaoEstadual { get; set; }
        public Endereco Endereco { get; set; }
        public bool Apagado { get; set; }

    }
    public class Endereco
    {
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }

        public override string ToString()
        {
            if (Numero > 0)
                return $"{Logradouro}, {Numero} - {Complemento}, {Bairro}, {Cidade}, {Cep}";
            else
                return Logradouro;
        }

    }
}