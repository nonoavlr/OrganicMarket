using MercadoCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercadoWeb.Models.AccountCreateModel
{
    public class AccountCreateModel
    {
        public string UserID { get; set; }
        public int IdProdutor { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public int IdContato { get; set; }
        public string NomeResponsavel { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int IdEndereco { get; set; }
        public string TipoLogradouro { get; set; }
        public string Logradouro { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }
        public int IdComprador { get; set; }
        public string CPF { get; set; }

        public Produtor GetAccountCreateModelProdutor(string userID)
        {
            return new Produtor
            {
                UserID = userID,
                IdProdutor = this.IdProdutor,
                RazaoSocial = this.RazaoSocial,
                CNPJ = this.CNPJ,
                Endereco = GetAccountCreateModelEndereco(userID),
                Contatos = new Contato[] { GetAccountCreateModelContato(userID) },
                IsAtivo = true
            };
        }

        private Contato GetAccountCreateModelContato(string userID)
        {
            return new Contato
            {
                UserID = userID,
                NomeResponsavel = this.NomeResponsavel,
                Telefone = this.Telefone,
                Email = this.Email
            };
        }

        private Endereco GetAccountCreateModelEndereco(string userID)
        {
            return new Endereco
            {
                UserID = userID,
                IdEndereco = this.IdEndereco,
                TipoLogradouro = this.TipoLogradouro,
                Logradouro = this.Logradouro,
                Estado = this.Estado,
                Cidade = this.Cidade,
                Bairro = this.Bairro,
                CEP = this.CEP,
            };
        }

        public Comprador GetAccountCreateModelComprador(string userID)
        {
            return new Comprador
            {
                UserID = userID,
                IdComprador = this.IdComprador,
                NomeResponsavel = this.NomeResponsavel,
                Telefone = this.Telefone,
                Email = this.Email,
                CPF = this.CPF,
                IsAtivo = true,
                Endereco = GetAccountCreateModelEndereco(userID)
            };
        }
    }
}
