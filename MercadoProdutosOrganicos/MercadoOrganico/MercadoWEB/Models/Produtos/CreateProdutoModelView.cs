using MercadoCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MercadoWeb.Models.Produtos
{
    public class CreateProdutoModelView
    {
        public string UserID { get; set; }
        public int IdProduto { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string Titulo { get; set; }

        [Required]
        public double Preco { get; set; }

        public string Descricao { get; set; }

        [Required]
        public int Quantidade { get; set; }
        public int IdProdutor { get; set; }
        public SelectList TipoProduto { get; set; }
        public int IdTipoProduto { get; set; }
        public SelectList TipoQuantidade { get; set; }
        public int IdTipoQuantidade { get; set; }

        public Produto GetProdutoObject (string userID, int idProdutor)
        {
            return new Produto
            {
                UserID = userID,
                IdProduto = this.IdProduto,
                IdTipoProduto = this.IdTipoProduto,
                IdTipoQuantidade = this.IdTipoQuantidade,
                Titulo = this.Titulo,
                Quantidade = this.Quantidade,
                Preco = this.Preco,
                Descricao = this.Descricao,
                IdProdutor = idProdutor
            };
        }
    }
}

