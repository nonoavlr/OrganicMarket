using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MercadoCore
{
    public class TipoProduto
    {
        public int IdTipoProduto { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(300)]
        public string Descricao { get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}
