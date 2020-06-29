using System;
using System.Collections.Generic;
using System.Text;

namespace MercadoCore
{
    public class Contato
    {
        public string UserID { get; set; }
        public int IdContato { get; set; }
        public string NomeResponsavel { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public Produtor Produtor { get; set; }
        public int IdProdutor { get; set; }
    }
}
