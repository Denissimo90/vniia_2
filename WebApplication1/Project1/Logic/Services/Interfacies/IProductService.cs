using ReportApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApp.Logic.Services.Interfacies
{
    public interface IProductService
    {
        void Insert(List<Product> products);
        void Insert(List<ProductQty> productQties);
        void DeleteProduct(int id);
        List<Product> GetProducts();
        Product GetProductById(int id);
        Product GetProductsByFactoryId(int factoryId);
    }
}
