﻿using _01_StoreQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceHost.ViewComponents
{
    public class LatestArrivalsViewComponent:ViewComponent
    {

        private readonly IProductQuery _productQuery;

        public LatestArrivalsViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public IViewComponentResult Invoke()
        {
           var LatestArrivals=_productQuery.GetLatestArrivals();
            return View(LatestArrivals);
        }
    }
    
}
