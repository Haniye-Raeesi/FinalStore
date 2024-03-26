using InventoryManagement.Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryApplication _inventoryApplication;

        public InventoryController(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }
        [HttpGet("Id")]
        List<InventoryOperationViewModel> GetOperationLog(long Id)
        {
            return _inventoryApplication.GetOperationLog(Id);
        }

    }
}
