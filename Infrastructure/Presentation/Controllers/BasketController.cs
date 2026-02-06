using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.DTOS.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize]
    public class BasketController(IServiceManager _serviceManager) : ApiController
    {
        //Get
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasketAsync(string id)
            => Ok(await _serviceManager.BasketService.GetBasketAsync(id));
        //Post 
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket)
            => Ok(await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basket));
        //Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBasketAsync(string id)
        {
           await _serviceManager.BasketService.DeleteBasketAsync(id);
            return NoContent();
        }
    }
}
