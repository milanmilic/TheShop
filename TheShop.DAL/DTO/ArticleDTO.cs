﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheShop.DAL.DTO
{
    public class ArticleDTO
    {
        public int ArticleId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public bool IsSold { get; set; }
        public DateTime? SoldDate { get; set; }
        public int? BuyerId { get; set; }
        public string BuyerName { get; set; }
    }
}
