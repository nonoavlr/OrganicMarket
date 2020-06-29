using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MercadoCore
{
    public class TipoQuantidade
    {
        public int IdTipoQuantidade { get; set; }
        public string Descricao { get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}
