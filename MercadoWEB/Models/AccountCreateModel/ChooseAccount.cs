using MercadoCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercadoWeb.Models.AccountCreateModel
{
    public class ChooseAccount
    {
        //Comprador = TRUE; Produtor = False
        public bool Tipo { get; set; }

        public ChooseAccount GetChooseAccount()
        {
            return new ChooseAccount
            {
                Tipo = this.Tipo
            };
        }
    }
}
