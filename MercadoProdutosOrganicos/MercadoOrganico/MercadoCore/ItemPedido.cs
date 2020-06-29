using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MercadoCore
{
    public class ItemPedido
    {
        public string UserID { get; set; }
        public int IdItemPedido { get; set; }

        [Required]
        public Produto Produto { get; set; }
        public int IdProduto { get; set; }
        public Pedido Pedido { get; set; }
        public int IdPedido { get; set; }
        public double SubTotal { get; set; }

        [Required]
        public float Quantidade { get; set; }
    }
}
