using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MercadoCore
{
    public class Produto
    {
        public string UserID { get; set; }
        public int IdProduto { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string Titulo { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(200)]
        public string Descricao { get; set; }

        [Required]
        public double Preco { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public Produtor Produtor { get; set; }
        public int IdProdutor { get; set; }

        [Required]
        public TipoProduto TipoProduto { get; set; }
        public int IdTipoProduto { get; set; }

        [Required]
        public TipoQuantidade TipoQuantidade { get; set; }
        public int IdTipoQuantidade { get; set; }

        public ICollection<ItemPedido> ItemPedido { get; set; }
    }
}
