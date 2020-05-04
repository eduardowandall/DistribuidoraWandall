using System;
using System.Collections.Generic;

namespace DistribuidoraWandall.DB
{
    public class Promocao
    {
        public int Id { get; set; }

        public DateTime DataValidade { get; set; }

        public bool Apagado { get; set; }

    }
}