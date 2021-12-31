using System;
using System.Collections.Generic;
using System.Text;

namespace KtuMarketApp.Models
{
    public class Product
    {
        public string ProductBarcode { get; set; }
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
        public DateTime PriceAddedDate { get; set; }
        public string PersonName { get; set; }
        public string MarketName { get; set; }
        public double ProductPrice { get; set; }

    }
}
