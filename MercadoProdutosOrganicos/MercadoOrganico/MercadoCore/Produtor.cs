using System;
using MercadoCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MercadoCore
{
    public class Produtor
    {
        public string UserID { get; set; }
        public int IdProdutor { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(200)]
        public string RazaoSocial { get; set; }

        [Required]
        [MinLength(14)]
        [MaxLength(15)]
        public string CNPJ { get; set; }
        public Endereco Endereco { get; set; }

        public int IdEndereco { get; set; }
        public ICollection<Contato> Contatos { get; set; }
        public ICollection<Produto> Produtos { get; set; }
        public bool IsAtivo { get; set; }
    }
}
