﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KtuMarketApp.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
        public DateTime PriceDate { get; set; }
        public string MarketName { get; set; }
        public double ProductPrice { get; set; }
    }
}