using System;
using System.Collections.Generic;
using System.Text;

namespace MercadoCore
{
    public class Comprador
    {
        public string UserID { get; set; }
        public int IdComprador { get; set; }
        public string NomeResponsavel { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public Endereco Endereco { get; set; }
        public int IdEndereco { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }
        public bool IsAtivo { get; set; }
    }
}
