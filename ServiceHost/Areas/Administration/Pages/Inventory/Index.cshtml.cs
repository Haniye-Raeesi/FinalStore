using InventoryManagement.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }
        public InventorySearchModel SearchModel;
        public List<InventoryViewModel> Invs;
        public SelectList Products;

        private readonly IProductApplication _productApplication;
        private readonly IInventoryApplication _inventoryApplication;

        public IndexModel(IProductApplication productApplication, IInventoryApplication inventoryApplication)
        {
            _productApplication = productApplication;
            _inventoryApplication = inventoryApplication;
        }

        public void OnGet(InventorySearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            Invs = _inventoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateInventory
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateInventory command)
        {
            var result = _inventoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var inventory = _inventoryApplication.GetDetails(id);
            inventory.Products = _productApplication.GetProducts();
            return Partial("Edit", inventory);
        }

        public JsonResult OnPostEdit(EditInventory command)
        {
            var result = _inventoryApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetIncrease(long id)
        {
            var command = new IncreaseInventory()
            {
                InventoryId = id
            };
            return Partial("Increase", command);
        }

        public JsonResult OnPostIncrease(IncreaseInventory command)
        {
            var result = _inventoryApplication.Increase(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetDecrease(long id)
        {
            var command = new DecreaseInventory()
            {
                InventoryId = id
            };
            return Partial("Decrease", command);
        }

        public JsonResult OnPostDecrease(DecreaseInventory command)
        {
            var result = _inventoryApplication.Decrease(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetOperationLog(long id)
        {
            var log = _inventoryApplication.GetOperationLog(id);
            return Partial("OperationLog", log);
        }
    }
}
