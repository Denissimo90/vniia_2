using ReportApp.Common;
using App.Entities;
using ReportApp.Logic.Repositories.Interfacies;

namespace ReportApp.Logic.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
