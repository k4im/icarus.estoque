using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace estoque.domain.Entity
{
    public class ProdutoDisponivel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
    }
}