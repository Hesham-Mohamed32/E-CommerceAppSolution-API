using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using Shared.DTOS.ProductModule;
using Shared.Enums;
using Shared.ErrorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
   
    public class ProductController(IServiceManager _serviceManager): ApiController
    {
        #region Get All Products
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProduct([FromQuery]ProductSpecificationParameters parameters)
        {
            var Products=await  _serviceManager.ProductService.GetAllProductAsync(parameters);
            return Ok(Products);
        }

        #endregion
        #region Get Product
        [ProducesResponseType(typeof(ProductDto) , StatusCodes.Status200OK)]
       
        [HttpGet("{id}")]
        public async Task<ActionResult>GetProductById(int id)
        {
            var Product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(Product);
        }
        #endregion
        #region Get All Types
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var Types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }
        #endregion
        #region Get All Brands
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllBrands()
        {
            var Brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }
        #endregion
    }
}
