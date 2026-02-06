using Shared;
using Shared.DTOS.ProductModule;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IProductService
    {
        //Get All Products
       Task<PaginatedResult<ProductDto>> GetAllProductAsync(ProductSpecificationParameters parameters);
      //Get BY Id
      Task<ProductDto> GetProductByIdAsync(int id);
        //GetAll Brands
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        //GetAll Types
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();

    }
}
