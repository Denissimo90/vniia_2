using ReportApp.Logic.Repositories.Interfacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApp.Common
{
    public interface IUnitOfWork : IDisposable
    {
        IManufactureRepository ManufactureRepository { get; }
        IProductQtyRepository ProductQtyRepository { get; }
        IProductRepository ProductRepository{ get; }
        IUserRepository UserRepository { get; }
        int Save();
    }
}
