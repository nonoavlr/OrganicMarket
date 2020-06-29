using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MercadoApplication
{
    public interface IEntityCrudHandler<T>
    {
        Task<T[]> Listar(string userID);

        Task<T> ObterUm(int id, string userID);

        Task<int> Inserir(T entity);

        Task<int> Alterar(int id, T entity, string userID);

        Task<int> Remover(int id, string userID);
    }
}
