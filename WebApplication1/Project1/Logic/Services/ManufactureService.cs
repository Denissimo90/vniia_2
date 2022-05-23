using ReportApp.Common;
using App.Entities;
using ReportApp.Logic.Services.Interfacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApp.Logic.Services
{
    public class ManufactureService : IManufactureService
    {
        IUnitOfWork uow;

        public ManufactureService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void DeleteFactory(int id)
        {
            var delProducts = uow.ProductRepository.GetEntities().Where(e => e.ManufactureId == id);
            foreach (var product in delProducts)
            {
                var delProductQty = uow.ProductQtyRepository.GetEntities().Where(q => q.Id == product.Id);
                foreach (var productQty in delProductQty)
                {
                    uow.ProductQtyRepository.Delete(productQty);
                }
                uow.ProductRepository.Delete(product);
            }
            uow.ManufactureRepository.Delete(id);
            uow.SaveAsync();
        }

        public Manufacture GetFactoryById(int id)
        {
            return uow.ManufactureRepository.GetEntity(id);
        }

        public List<Manufacture> GetFactyries()
        {
            return null;
            //return uow.ManufactureRepository.GetEntities();
        }

        public void Insert(Manufacture facture)
        {
            uow.ManufactureRepository.Add(facture);
            uow.SaveAsync();
        }
    }
}
