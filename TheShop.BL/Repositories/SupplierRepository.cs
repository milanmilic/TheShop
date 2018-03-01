using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheShop.BL.IRepositories;
using TheShop.DAL.DTO;
using TheShop.DATA.Model;

namespace TheShop.BL.Repositories
{
    public class SupplierRepository : BaseRepository, ISupplierRepository, IDisposable
    {
        public IEnumerable<SupplierDTO> GetSuppliers()
        {
            List<SupplierDTO> supplierDTOList = new List<SupplierDTO>();

            try
            {
                var supplier_db = ctx.Suppliers.ToList();

                supplier_db.ForEach(item => supplierDTOList.Add(new SupplierDTO
                {
                    SupplierId = item.SupplierId,
                    Name = item.Name,
                    ArticleDTOList = item.Articles.Select(x => new ArticleDTO { ArticleId = x.ArticleId, BuyerId = x.BuyerId, IsSold = x.IsSold, Name = x.Name, Price = x.ArticlePrice, SoldDate = x.SoldDate }).ToList()
                }));

                return supplierDTOList;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error - {ex.Message} - on getting suppliers list.");
            }
        }

        public SupplierDTO GetSupplierById(int supplierId)
        {
            SupplierDTO supplierDTO = new SupplierDTO();

            try
            {
                var supplier_db = ctx.Suppliers.Find(supplierId);

                if(supplier_db != null)
                {
                    supplierDTO.SupplierId = supplier_db.SupplierId;
                    supplierDTO.Name = supplier_db.Name;
                    supplierDTO.ArticleDTOList = supplier_db.Articles.Select(x => new ArticleDTO { ArticleId = x.ArticleId, BuyerId = x.BuyerId, IsSold = x.IsSold, Name = x.Name, Price = x.ArticlePrice, SoldDate = x.SoldDate }).ToList();
                }
                return supplierDTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error - {ex.Message} - on getting supplier by id.");
            }
        }

        public bool InsertSupplier(SupplierDTO supplier)
        {
            try
            {
                var supplier_db = ctx.Set<Supplier>();
                Supplier newSupplier = new Supplier();

                if(supplier != null)
                {
                    newSupplier.SupplierId = supplier.SupplierId;
                    newSupplier.Name = supplier.Name;

                    ctx.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error - {ex.Message} - on inserting new supplier.");
            }
        }

        public bool UpdateSupplier(SupplierDTO supplier)
        {
            try
            {
                var supplier_db = ctx.Suppliers.Where(x => x.SupplierId == supplier.SupplierId).FirstOrDefault();

                if(supplier_db != null)
                {
                    supplier_db.SupplierId = supplier.SupplierId;
                    supplier_db.Name = supplier.Name;

                    ctx.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error - {ex.Message} - on updating existing supplier.");
            }
        }

        public bool RemoveSupplier(int supplierId)
        {
            try
            {
                Supplier supplier_db = ctx.Suppliers.Find(supplierId);

                if(supplier_db != null)
                {
                    ctx.Suppliers.Remove(supplier_db);

                    ctx.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error - {ex.Message} - on removing existing supplier.");
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    ctx.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
