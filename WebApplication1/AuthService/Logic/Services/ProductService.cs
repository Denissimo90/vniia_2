using AuthService.Common;
using AuthService.Entities;
using AuthService.Logic.Services.Interfacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Logic.Services
{
    public class ProductService : IProductService
    {
        IUnitOfWork uow;

        public ProductService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void DeleteProduct(int id)
        {
            uow.ProductRepository.Delete(id);
            uow.SaveAsync();
        }

        public List<Product> GetProducts()
        {
            return null;
            //return uow.ProductRepository.GetEntities();
        }

        public Product GetProductsByFactoryId(int factoryId)
        {
            throw new NotImplementedException();
        }

        public Product GetProductById(int id)
        {
            return uow.ProductRepository.GetEntity(id);
        }

        public void Insert(Product product)
        {
            
        }

        public void Insert(List<Product> products)
        {
            foreach (var item in products)
            {
                item.Qties = null;
                uow.ProductRepository.Add(item);
            }
            uow.SaveAsync();
        }

        public void Insert(List<ProductQty> productQties)
        {
            foreach (var item in productQties)
            {
                item.Product = null;
                uow.ProductQtyRepository.Add(item);
            }
            uow.SaveAsync();
        }
    }
}
