using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheShop.DAL.DTO
{
    public class SupplierDTO
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public List<ArticleDTO> ArticleDTOList { get; set; }
    }
}
