using CheapListBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheapListBackEnd.RepositoryInterfaces
{
    public interface IAppProductRepository
    {
        IEnumerable<AppProduct> GetProductCart(int listID);
        int PostAppProduct(AppProduct product);
        int DeleteProduct(string barcode, int listID);
        int UpdateQuantity(AppProduct appProduct, bool HasAddedQ); //action = ture => plus





    }
}
