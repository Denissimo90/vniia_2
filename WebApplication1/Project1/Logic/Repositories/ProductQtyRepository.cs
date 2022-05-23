using ReportApp.Common;
using App.Entities;
using ReportApp.Logic.Repositories.Interfacies;

namespace ReportApp.Logic.Repositories
{
    public class ProductQtyRepository : BaseRepository<ProductQty>, IProductQtyRepository
    {
        public ProductQtyRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
