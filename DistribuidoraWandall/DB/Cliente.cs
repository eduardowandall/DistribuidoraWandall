using System;
using System.Collections.Generic;

namespace DistribuidoraWandall.DB
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Bairro { get; set; }
        public string NumeroTelefone { get; set; }
        public bool Apagado { get; set; }

    }
}