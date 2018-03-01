using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheShop.DAL.DTO;

namespace TheShop.BL.IRepositories
{
    public interface ISupplierRepository : IDisposable
    {
        IEnumerable<SupplierDTO> GetSuppliers();
        SupplierDTO GetSupplierById(int supplierId);
        bool InsertSupplier(SupplierDTO supplier);
        bool UpdateSupplier(SupplierDTO supplier);
        bool RemoveSupplier(int supplierId);
    }
}
