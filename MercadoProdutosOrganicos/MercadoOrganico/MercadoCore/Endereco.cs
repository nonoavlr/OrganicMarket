using System;
using System.Collections.Generic;
using System.Text;

namespace MercadoCore
{
    public class Endereco
    {
        public string UserID { get; set; }
        public int IdEndereco { get; set; }
        public string TipoLogradouro { get; set; }
        public string Logradouro { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }
        public Comprador Comprador { get; set; }
        public Produtor Produtor { get; set; }
    }
}
