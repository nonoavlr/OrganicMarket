using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MercadoCore
{
    public class Pedido
    {
        public string UserID { get; set; }
        public int IdPedido { get; set; }
        public Comprador Comprador { get; set; }
        public int IdComprador { get; set; }
        public double PrecoTotal { 
            get { return this.ItemPedidos.Sum(i => i.SubTotal); } 
        }
        public ICollection<ItemPedido> ItemPedidos { get; set; }
    }
}
